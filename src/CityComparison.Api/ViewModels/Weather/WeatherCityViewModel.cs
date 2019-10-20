using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityComparison.Api.ViewModels
{
    public class WeatherCityViewModel
    {
        public Guid? Id { get; set; }

        public string CityA { get; set; }

        public string CityB { get; set; }

        public Guid UserId { get; set; }
    }
}
