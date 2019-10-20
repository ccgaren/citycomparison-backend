using Moq;
using Xunit;
using CityComparison.Application.Services;
using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;


namespace CityComparison.Application.Test
{
    public class UserAppServiceTest
    {
        [Fact]
        public void When_User_Exists_Then_IsEmailUniq_Should_Be_True()
        {
            //Arrange
            var user = new User { Email = "test@test.test" };
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.IsEmailUniq(user.Email)).Returns(true);
            var userAppservice = new UserAppService(mockUserRepo.Object);            
            //Act
            var result = userAppservice.IsEmailUniq(user.Email);           
            // Assert
            Assert.True(result);
        }
    }
}
