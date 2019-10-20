using System;
using Xunit;
using CityComparison.Application.Authorization;
using CityComparison.Domain.Entites;


namespace CityComparison.Application.Test
{
    public class AuthServiceTest
    {
        [Theory]
        [InlineData("PR7fOkA+oEWp/vEBHZ4hvw==", 6000)]
        [InlineData("uTtud2dBF0SlXp7MKyL4FQCJIzQW/BK06PrxgeWtkKtg", 25089)]     
        public void When_User_GetAuthData_Then_AuthData_Should_Be_Returned(string jWTSecretKey, int jWTLifespan)
        {
            //Arrange
            var user = new User { Id =Guid.Parse("109E95DF-D485-45B0-8E4A-3580F35A184F") };
            var authService = new AuthService();
            authService.SetKey(jWTSecretKey, jWTLifespan);
            //Act
            var result = authService.GetAuthData(user.Id);          
            // Assert
            Assert.NotNull(result.Token);
            Assert.Equal(((DateTimeOffset)DateTime.UtcNow.AddSeconds(jWTLifespan)).ToUnixTimeSeconds(), result.TokenExpirationTime);
            Assert.Equal(user.Id.ToString(), result.Id);
        }
    }
}
