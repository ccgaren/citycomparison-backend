using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoHelper;
using Microsoft.IdentityModel.Tokens;
using CityComparison.Application.Authorization.Models;

namespace CityComparison.Application.Authorization
{
    public class AuthService : IAuthService
    {
        string jwtSecret;
        int jwtLifespan;

        /// <inheritdoc />
        public void SetKey(string jwtSecret, int jwtLifespan)
        {
            this.jwtSecret = jwtSecret;
            this.jwtLifespan = jwtLifespan;
        }
        /// <inheritdoc />
        public AuthData GetAuthData(Guid id)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(jwtLifespan);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
               {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = expirationTime,

                SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                   SecurityAlgorithms.HmacSha256Signature
               )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = id.ToString()
            };
        }

        /// <inheritdoc />
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        /// <inheritdoc />
        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }
    }
}
