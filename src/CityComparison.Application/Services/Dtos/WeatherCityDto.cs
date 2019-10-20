using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.Services.Dtos
{
    public class WeatherCityDto
    {
        public Guid? Id { get; set; }
        public string CityA { get; set; }
        public string CityB { get; set; }
        public Guid UserId { get; set; }
    }
}
