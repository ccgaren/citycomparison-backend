using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.Services.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
