using AutoMapper;
using CityComparison.Application.Services.Dtos;
using CityComparison.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
