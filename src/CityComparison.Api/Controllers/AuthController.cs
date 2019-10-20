using System;
using System.Net.Mime;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CityComparison.Application.Authorization;
using CityComparison.Application.Services;
using CityComparison.Application.Authorization.Models;
using CityComparison.Api.ViewModels;
using CityComparison.Application.Services.Dtos;


namespace CityComparison.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserAppService _userAppService;
 

        public AuthController(IAuthService authService, IUserAppService userAppService, IConfiguration Configuration)
        {
            _authService = authService;
            _userAppService = userAppService;
            _authService.SetKey(Configuration.GetValue<string>("JWTSecretKey"), Configuration.GetValue<int>("JWTLifespan"));
        }

        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<AuthData> Post([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Log.Information("getting user");
                var user = _userAppService.GetUser(model.Email);

                if (user == null)
                {
                    return BadRequest(new { email = "no user with this email" });
                }

                var passwordValid = _authService.VerifyPassword(model.Password, user.Password);
                if (!passwordValid)
                {
                    return BadRequest(new { password = "invalid password" });
                }

                return _authService.GetAuthData(user.Id);
            }
            catch (Exception e)
            {
                var message = "An error has occurred on login.";
                Log.Error(string.Format("{0} {1}", message, e));
                return new ActionWithMessageResult(message);
            }
        }

        [HttpPost("register")]
        //[ValidateAntiForgeryToken]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<AuthData> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var emailUniq = _userAppService.IsEmailUniq(model.Email);
                if (!emailUniq) return BadRequest(new { email = "user with this email already exists" });

                var userDto = new UserDto
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    Password = _authService.HashPassword(model.Password)
                };

                _userAppService.SaveUser(userDto);

                return _authService.GetAuthData(userDto.Id);
            }
            catch (Exception e)
            {
                var message = "An error has occurred on register.";
                Log.Error(string.Format("{0} {1}", message, e));
                return new ActionWithMessageResult(message);
            }
         
        }
    }
}
