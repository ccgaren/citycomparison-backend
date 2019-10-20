using CityComparison.Application.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.Authorization
{
    public interface IAuthService
    {
        /// <summary>
        /// Set key and lifespan for JWT
        /// </summary>
        /// <param name="jwtSecret"></param>
        /// <param name="jwtLifespan"></param>
        void SetKey(string jwtSecret, int jwtLifespan);

        /// <summary>
        /// encrypt password 
        /// </summary>
        /// <param name="password"></param>
        /// <returns>encrypted password</returns>
        string HashPassword(string password);
        
        /// <summary>
        /// Compare passwords
        /// </summary>
        /// <param name="actualPassword"></param>
        /// <param name="hashedPassword"></param>
        /// <returns>The compared result </returns>
        bool VerifyPassword(string actualPassword, string hashedPassword);

        /// <summary>
        /// Get auth data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AuthData</returns>
        AuthData GetAuthData(Guid id);
    }
}
