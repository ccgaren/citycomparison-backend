using Autofac;
using CityComparison.EntityFrameworkCore.Repositories;

namespace CityComparison.EntityFrameworkCore.DI
{
    public class EntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                .PropertiesAutowired()
                .AsImplementedInterfaces();

            builder.RegisterType<UserRepository>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();

            builder.RegisterType<WeatherCityRepository>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();
        }
    }
}
