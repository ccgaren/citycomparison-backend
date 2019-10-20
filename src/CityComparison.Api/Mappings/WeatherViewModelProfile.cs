using AutoMapper;
using CityComparison.Api.ViewModels;
using CityComparison.Application.Services.Dtos;

namespace CityComparison.Api.Mappings
{
    public class WeatherViewModelProfile : Profile
    {
        public WeatherViewModelProfile()
        {
            CreateMap<WeatherCityViewModel, WeatherCityDto>().ReverseMap();
        }
    }
}
