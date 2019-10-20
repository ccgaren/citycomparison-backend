using CityComparison.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Domain.Repositories
{
    public interface IWeatherCityRepository
    {
        /// <summary>
        /// Get WeatherCities By userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Weather Cities</returns>
        IEnumerable<WeatherCity> GetWeatherCities(Guid userId);

        /// <summary>
        /// Save weatherCity
        /// </summary>
        /// <param name="weatherCity"></param>
        void Save(WeatherCity weatherCity);
    }
}
