using System;
using System.Collections.Generic;
using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;

namespace CityComparison.EntityFrameworkCore.Repositories
{
    public class WeatherCityRepository : EntityBaseRepository<WeatherCity>, IWeatherCityRepository
    {
        public WeatherCityRepository(CityComparisonContext context) : base(context) { }

        /// <inheritdoc />
        public IEnumerable<WeatherCity> GetWeatherCities(Guid userId)
        {
            return FindBy(x => x.UserId == userId);
        }

        /// <inheritdoc />
        public void Save(WeatherCity weatherCity)
        {
            Insert(weatherCity);
            Commit();
        }
    }
}
