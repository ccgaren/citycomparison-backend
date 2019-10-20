using AutoMapper;
using CityComparison.Application.Services.Dtos;
using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;

namespace CityComparison.Application.Services
{
    public class UserAppService : IUserAppService
    {
        IUserRepository _userRepository;

        public IMapper Mapper { get; set; }

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public bool IsEmailUniq(string email)
        {
            return _userRepository.IsEmailUniq(email);
        }

        /// <inheritdoc />
        public UserDto GetUser(string email)
        {
            var user = _userRepository.GetUser(email);

            return Mapper.Map<UserDto>(user);
        }

        /// <inheritdoc />
        public void SaveUser(UserDto userDto)
        {
            var user = Mapper.Map<User>(userDto);
            _userRepository.Save(user);
        }
    }
}
