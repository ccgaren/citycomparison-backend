using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CityComparison.Domain.Entites;

namespace CityComparison.EntityFrameworkCore
{
    public class CityComparisonContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<WeatherCity> WeatherCities { get; set; }

        public CityComparisonContext(DbContextOptions<CityComparisonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForUser(modelBuilder.Entity<User>());
            ConfigureModelBuilderForWeatherCity(modelBuilder.Entity<WeatherCity>());
        }

        void ConfigureModelBuilderForUser(EntityTypeBuilder<User> configuration)
        {
            configuration.ToTable("User");
            configuration.HasKey(x => x.Id);
            configuration.Property(x => x.Email).HasMaxLength(60).IsRequired();
            configuration.Property(x => x.Password).IsRequired();
        }

        void ConfigureModelBuilderForWeatherCity(EntityTypeBuilder<WeatherCity> configuration)
        {
            configuration.ToTable("WeatherCity");
            configuration.HasKey(x => x.Id);
            configuration.Property(x => x.CityA).HasMaxLength(60).IsRequired();
            configuration.Property(x => x.CityB).HasMaxLength(60);
            configuration.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
