using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;
using CityComparison.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace CityComparison.EntityFrameworkCore.Test
{
    public class UserRepositoryTest
    {

        public UserRepositoryTest()
        {

        }
        [Fact]
        public void When_User_Exits_Then_IsEmailUniq_Should_Be_True()
        {
            //arrange
            var user = new User { Id= Guid.Parse("5701DD72-BBBE-4282-9838-AD52184CBEEF"), Email="test@test.test" };
            var mockContext = new Mock<CityComparisonContext>();
            var mockUserDbSet = new Mock<DbSet<User>>();

           // mockUserDbSet.Setup(x => x.Add(user));

            mockContext.Setup(m => m.Set<User>()).Returns(mockUserDbSet.Object);

            var userRepo = new UserRepository(mockContext.Object);

            var result = userRepo.IsEmailUniq(user.Email);

            Assert.True(result);
        }
    }
}
