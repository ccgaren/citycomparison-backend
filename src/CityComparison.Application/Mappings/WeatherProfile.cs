using AutoMapper;
using CityComparison.Application.Services.Dtos;
using CityComparison.Domain.Entites;

namespace CityComparison.Application.Mappings
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherCityDto, WeatherCity>();
            CreateMap<WeatherCity, WeatherCityDto>();
        }
    }
}
