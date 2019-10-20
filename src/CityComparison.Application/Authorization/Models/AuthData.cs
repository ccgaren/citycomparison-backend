using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.Authorization.Models
{
    public class AuthData
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public string Id { get; set; }
    }
}
