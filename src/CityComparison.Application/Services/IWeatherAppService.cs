using System;
using System.Collections.Generic;
using CityComparison.Application.Services.Dtos;

namespace CityComparison.Application.Services
{
    public interface IWeatherAppService
    {
        /// <summary>
        /// Get WeatherCities by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> list of the user's WeatherCities</returns>
        IEnumerable<WeatherCityDto> GetWeatherCities(Guid userId);

        /// <summary>
        /// Save WeatherCity
        /// </summary>
        /// <param name="weatherCityDto"></param>
        void SaveWeatherCity(WeatherCityDto weatherCityDto);
    }
}
