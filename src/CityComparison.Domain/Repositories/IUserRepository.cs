using CityComparison.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Check if the user exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailUniq(string email);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns> user </returns>
        User GetUser(string email);

        /// <summary>
        ///  Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> user </returns>
        User GetUserById(Guid id);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="user"></param>
        void Save(User user);
    }
}
