using System;
using CityComparison.Domain.Entites;
using CityComparison.Domain.Repositories;

namespace CityComparison.EntityFrameworkCore.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(CityComparisonContext context) : base(context) { }

        /// <inheritdoc />
        public bool IsEmailUniq(string email)
        {
            var user = this.Get(u => u.Email == email);
            return user == null;
        }

        /// <inheritdoc />
        public User GetUser(string email)
        {
            var user = this.Get(u => u.Email == email);
            return user;
        }

        /// <inheritdoc />
        public User GetUserById(Guid id)
        {
            var user = this.Get(u => u.Id == id);
            return user;
        }

        /// <inheritdoc />
        public void Save(User user)
        {
            this.Insert(user);
            this.Commit();
        }
    }
}
