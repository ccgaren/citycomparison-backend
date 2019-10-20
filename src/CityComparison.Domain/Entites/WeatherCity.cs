using System;

namespace CityComparison.Domain.Entites
{
    public class WeatherCity : IEntityBase
    {
        public Guid Id { get; set; }
        public string CityA { get; set; }
        public string CityB { get; set; }
        public Guid UserId { get; set; }

        virtual public User User { get; set; }
    }
}
