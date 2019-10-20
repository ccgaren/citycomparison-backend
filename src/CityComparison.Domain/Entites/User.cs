using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Domain.Entites
{
    public class User : IEntityBase
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
