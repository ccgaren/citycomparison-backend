using Autofac;
using CityComparison.Application.Authorization;
using CityComparison.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Application.DI
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();

            builder.RegisterType<UserAppService>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();

            builder.RegisterType<WeatherAppService>()
                .PropertiesAutowired()
                .AsImplementedInterfaces();

        }
    }
}
