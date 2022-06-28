using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginApiService.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LoginApiService.Utils;
using Microsoft.AspNetCore.Http;
using LoginApiService.Services;
using SharedModels.Utils;

namespace LoginApiService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        public IConfiguration Configuration { get; }

        private IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository repository)
        {
            Configuration = configuration;
            _userRepository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(UserRequest user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Incorrect username and password"
                    });
                }

                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var userInfo = _userRepository.Register(new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsActive = true
                });

                return Ok(new ApiResponseStatus
                {
                    StatusCode = StatusCodes.Status201Created,
                    Status = "User added successfully."
                });
            }
            catch(Exception ex)
            {
                return  Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(UserRequest request)
        {
            try
            {
                // access data from db.
                var userData = _userRepository.Login(new User
                {
                    EmailId = request.EmailId
                });

                if (userData == null)
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid User Credentials."
                    });
                }

                if (!ValidatePassword(request.Password, userData.PasswordHash, userData.PasswordSalt))
                {
                    return BadRequest(new ApiResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Status = "Invalid User Credentials"
                    });
                }

                var token = GenerateToken(userData);

                var refreshToken = GenerateRefreshToken(userData.UserRecId);
                SetRefreshToken(refreshToken, userData.UserRecId);

                return Ok(new { 
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        [HttpGet("RefreshToken")]
        public ActionResult RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                // access data from db.
                var userData = _userRepository.GetToken(refreshToken);

                if (userData == null || !userData.Token.Equals(refreshToken))
                {
                    return Unauthorized(new ApiResponseStatus() { 
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Status = "Unauthorized Token."
                    });
                }
                else if (userData.Expires < DateTime.Now)
                {
                    return Unauthorized(new ApiResponseStatus()
                    {
                        StatusCode = StatusCodes.Status510NotExtended,
                        Status = "Token has expired."
                    });
                }

                var token = GenerateToken(userData.User);
                var newRefreshToken = GenerateRefreshToken(userData.UserRecId);
                SetRefreshToken(newRefreshToken, userData.UserRecId);

                return Ok(new
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
        }

        // Private Methods //
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private RefreshToken GenerateRefreshToken(int userRecId)
        {
            var bytes = BitConverter.GetBytes(userRecId);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(bytes),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken refreshToken, int userRecId)
        {
            var cookiesOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            // Update token into datatbase
            _userRepository.RefreshToken(new UserToken
            {
                Token = refreshToken.Token,
                UserRecId = userRecId,
                Expires = refreshToken.Expires,
                Created = refreshToken.Created,
            });

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiesOptions);
        }

        private bool ValidatePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var compute = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return compute.SequenceEqual(passwordHash);
            }
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, (user.FirstName + " " + user.LastName)),
            };

            claims.Add(new Claim("UserRecId", user.EmailId));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, RoleConstant.Admin));
                claims.Add(new Claim("Role", RoleConstant.Admin));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, RoleConstant.User));
                claims.Add(new Claim("Role", RoleConstant.User));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:SecretKey").Value));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expiryTime = Convert.ToInt32(Configuration["Jwt:ExpiryTimeInMinutes"]);
            var token = new JwtSecurityToken(
                claims: claims, 
                expires: DateTime.Now.AddDays(expiryTime), 
                signingCredentials: signInCred
            );

            var secureToken = new JwtSecurityTokenHandler().WriteToken(token);

            return secureToken;
        }
    }
}
