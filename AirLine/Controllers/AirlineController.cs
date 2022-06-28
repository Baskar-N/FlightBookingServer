using AirlineApiService.Services;
using AirlineApiService.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels.Models;
using SharedModels.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _airlineRepository;

        private IWebHostEnvironment _webHostEnvironment;

        public AirlineController(IAirlineRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _airlineRepository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        // Register the new airline details.
        [HttpPost("Register")]
        public ActionResult Register(IFormCollection data, IFormFile imageFile)
        {
            try
            {
                var register = JsonConvert.DeserializeObject<Airline>(data["airline"]);

                if (register == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                var fileName = "";

                try
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                    if (imageFile.Length > 0)
                    {
                        var extension = Path.GetExtension(imageFile.FileName);
                        fileName = Guid.NewGuid().ToString() + extension;

                        if(!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                        {
                            imageFile.CopyTo(fileStream);
                        }

                        register.Logo = fileName;
                    }
                }
                catch (Exception ex)
                {

                }

                // update data into database
                _airlineRepository.Register(register);

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status201Created,
                    Status = string.Format(AppConstant.AirlineRegisterMsg, register.Name)
                });
            }
            catch(Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        // Get all airline details
        [HttpGet("GetAllAirline"), AllowAnonymous]
        public ActionResult GetAllAirline(bool isShowInactiveAlso = false)
        {
            try
            {
                // get data from database
                var airlinelist = _airlineRepository.GetAllAirline(isShowInactiveAlso).ToList();

                return Ok(airlinelist);
            }
            catch(Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        // Block airline details
        [HttpGet("BlockAirline")]
        public ActionResult BlockAirline(int airlineRecId)
        {
            try
            {
                // update data into database
                var airline = _airlineRepository.BlockAirline(airlineRecId);

                if(airline == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = AppConstant.NoAirlineFound
                    });
                }

                var status = airline.IsActive ? AppConstant.AirlineUnblockedMsg : AppConstant.AirlineBlockedMsg;

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status200OK,
                    Status = string.Format(status, airline.Name)
                });
            }
            catch(Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }
    }
}
