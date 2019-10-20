using System;
using System.Net.Mime;
using Serilog;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CityComparison.Application.Services;
using CityComparison.Api.ViewModels;
using CityComparison.Application.Services.Dtos;
using System.Collections.Generic;

namespace CityComparison.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherAppService _weatherAppService;
        private readonly IMapper _mapper;

        public WeatherController(IWeatherAppService weatherAppService, IMapper mapper)
        {
            _weatherAppService = weatherAppService;
            _mapper = mapper;

        }

        [HttpPost("cities")]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody]WeatherCityViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var weatherCityDto = _mapper.Map<WeatherCityDto>(model);
                _weatherAppService.SaveWeatherCity(weatherCityDto);
                var weatherCityDtos = _weatherAppService.GetWeatherCities(model.UserId);

                return Ok(new ApiResponse<IEnumerable<WeatherCityViewModel>>(_mapper.Map<IEnumerable<WeatherCityViewModel>>(weatherCityDtos)));
            }
            catch (Exception e)
            {
                var message = "An error has occurred on saving weather cities.";
                Log.Error(string.Format("{0} {1}", message, e));
                return new ActionWithMessageResult(message);
            }


        }


        [HttpGet("{userId}/cities")]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<IEnumerable<WeatherCityViewModel>> GetCitiesByUserId(Guid userId)
        {
            try
            {
                var weatherCitiesDto = _weatherAppService.GetWeatherCities(userId);

                var weatherCities = _mapper.Map<IEnumerable<WeatherCityViewModel>>(weatherCitiesDto);

                return Ok(new ApiResponse<IEnumerable<WeatherCityViewModel>>(weatherCities));
            }
            catch (Exception e)
            {
                var message = "An error has occurred on GetCities.";
                Log.Error(string.Format("{0} {1}", message, e));
                return new ActionWithMessageResult(message);
            }

          
        }

    }
}
