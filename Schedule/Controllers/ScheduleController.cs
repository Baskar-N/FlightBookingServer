using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScheduleApiService.Models;
using ScheduleApiService.Services;
using SharedModels.Models;
using SharedModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScheduleApiService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepositroy _scheduleRepository;

        public ScheduleController(IScheduleRepositroy repository)
        {
            _scheduleRepository = repository;
        }

        [HttpPost("AddInventory")]
        public IActionResult AddInventory(ScheduleDto schedule)
        {
            try
            {
                if (schedule == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                // update data into database
                _scheduleRepository.AddInventory(new SharedModels.Models.Schedule { 
                    AirlineId = schedule.AirlineId,
                    FlightNumber = schedule.FlightNumber,
                    FromPlace = schedule.FromPlace,
                    ToPlace = schedule.ToPlace,
                    StartDateTime = schedule.StartDateTime,
                    EndDateTime = schedule.EndDateTime,
                    ScheduledDaysRecId = schedule.ScheduledDaysRecId,
                    InstrumentUsed = schedule.InstrumentUsed,
                    Bcs = schedule.Bcs,
                    NonBcs = schedule.NonBcs,
                    TicketCost = schedule.TicketCost,
                    MealTypeRecId = schedule.MealTypeRecId,
                    IsActive = true
                });

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status201Created,
                    Status = "New schedule added successfully"
                });
            }
            catch(Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpPost("Search")]
        public IActionResult Search(ScheduleDto filter)
        {
            try
            {
                if (filter == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                var returnDataSet = new Dictionary<string, object>();

                // get data from database
                var searchResult = _scheduleRepository.Search(new SharedModels.Models.Schedule
                {
                   FromPlace = filter.FromPlace,
                   ToPlace = filter.ToPlace,
                   StartDateTime = filter.StartDateTime
                });

                returnDataSet.Add("Journey", searchResult);

                if (filter.EndDateTime != null)
                {
                    var returnJourney = _scheduleRepository.Search(new SharedModels.Models.Schedule
                    {
                        FromPlace = filter.ToPlace,
                        ToPlace = filter.FromPlace,
                        StartDateTime = filter.EndDateTime
                    });

                    returnDataSet.Add("ReturnJourney", returnJourney);
                }

                return Ok(JsonConvert.SerializeObject(returnDataSet, new JsonSerializerSettings() { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetAllSchedules")]
        public IActionResult GetAllSchedules(bool isNeedRelatedLookup = false)
        {
            try
            {
                // get data from database
                var schedules = _scheduleRepository.GetAllSchedule();

                var data = new Dictionary<string, object>();

                data.Add("Schedules", schedules);

                if(isNeedRelatedLookup)
                {
                    data.Add("MealTypes", _scheduleRepository.GetMealTypes());
                    data.Add("Places", _scheduleRepository.GetPlaces());
                    data.Add("ScheduledTypes", _scheduleRepository.GetScheduledTypes());
                }

                return Ok(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpPost("AddDiscount")]
        public IActionResult AddDiscount(Discount discount)
        {
            try
            {
                if (discount == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                // update data into database
                _scheduleRepository.AddDiscount(new Discount
                {
                    DiscountCode = discount.DiscountCode,
                    DiscountAmount = discount.DiscountAmount,
                    IsActive = true
                });

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status201Created,
                    Status = "New discount added successfully"
                });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetDiscount")]
        public IActionResult GetDiscount(string discountCode)
        {
            try
            {
                if (discountCode == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid request data."
                    });
                }

                // get data from database
                var discount = _scheduleRepository.GetDiscount(discountCode);

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetAllDiscount")]
        public IActionResult GetAllDiscount()
        {
            try
            {
                // get data from database
                var discount = _scheduleRepository.GetAllDiscount();

                return Ok(discount);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetAllMealType")]
        public IActionResult GetAllMealType()
        {
            try
            {
                // get data from database
                var mealTypeList = _scheduleRepository.GetMealTypes();

                return Ok(mealTypeList);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("GetAllPlace")]
        public IActionResult GetAllPlaces()
        {
            try
            {
                // get data from database
                var mealTypeList = _scheduleRepository.GetPlaces();

                return Ok(mealTypeList);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }
    }
}
