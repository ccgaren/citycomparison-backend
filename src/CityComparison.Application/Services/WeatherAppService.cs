using System;
using System.Collections.Generic;
using AutoMapper;
using CityComparison.Application.Services.Dtos;
using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;

namespace CityComparison.Application.Services
{
    public class WeatherAppService : IWeatherAppService
    {
        IWeatherCityRepository _weatherCityRepository;

        public IMapper Mapper { get; set; }

        public WeatherAppService(IWeatherCityRepository weatherCityRepository)
        {
            _weatherCityRepository = weatherCityRepository;
        }

        /// <inheritdoc />
        public IEnumerable<WeatherCityDto> GetWeatherCities(Guid userId)
        {
            var weatherCities = _weatherCityRepository.GetWeatherCities(userId);
            return Mapper.Map<IEnumerable<WeatherCityDto>>(weatherCities);
        }

        /// <inheritdoc />
        public void SaveWeatherCity(WeatherCityDto weatherCityDto)
        {
            if (weatherCityDto.Id==null || weatherCityDto.Id == Guid.Empty)
            {
                weatherCityDto.Id = Guid.NewGuid();
            }

            var weatherCity = Mapper.Map<WeatherCity>(weatherCityDto);
            _weatherCityRepository.Save(weatherCity);
        }
    }
}
