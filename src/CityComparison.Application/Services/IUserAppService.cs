using CityComparison.Application.Services.Dtos;

namespace CityComparison.Application.Services
{
    public interface IUserAppService
    {
        /// <summary>
        /// Check if the user exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>the checked result</returns>
        bool IsEmailUniq(string email);

        /// <summary>
        ///  Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>userDto</returns>
        UserDto GetUser(string email);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="userDto"></param>
        void SaveUser(UserDto userDto);
    }
}
