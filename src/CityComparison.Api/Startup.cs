using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using CityComparison.EntityFrameworkCore;

namespace CityComparison.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;


        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;

            var builder = new ConfigurationBuilder()
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
            Configuration.Bind(configuration);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    });
            
            services.AddCors();

            services.AddRouting(options => options.LowercaseUrls = true);

            services
               .AddEntityFrameworkSqlServer()
               .AddDbContext<CityComparisonContext>(options =>
               {
                   options.UseSqlServer(Configuration.GetConnectionString("CityComparisonContext"));
               }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            services.AddAutoMapper(typeof(Startup));

            if (EnableSwagger())
            {
                // Register the Swagger generator, defining one or more Swagger documents
                // reference Swashbuckle.AspNetCore.SwaggerGen
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "CityComparison API", Version = "v1" });
                    c.DescribeAllEnumsAsStrings();
                });
            }

            //create a container for autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);


            builder = RegisterModules(builder);
            builder = RegisterAutoMappings(builder);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc();

            loggerFactory.AddSerilog();

            if (EnableSwagger())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CityComparison API V1");
                });
            }
        }

        private bool EnableSwagger()
        {
            var enableSwagger = Configuration.GetValue("EnableSwagger", false);
            return enableSwagger || _environment.IsDevelopment();
        }

        //Register DI
        private ContainerBuilder RegisterModules(ContainerBuilder builder)
        {
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(string.Empty)
             .Where(a => a.FullName.StartsWith("CityComparison"));

            foreach (var assembly in assemblyNames)
            {
                AppDomain.CurrentDomain.Load(assembly);
            }

            var abc = AppDomain.CurrentDomain.GetAssemblies();
            var cityComparisonAssemblies = new List<Assembly>();

            cityComparisonAssemblies.AddRange(AppDomain.CurrentDomain
                        .GetAssemblies()
                        .Where(a => a.FullName.StartsWith("CityComparison")));

            builder.RegisterAssemblyModules(cityComparisonAssemblies.ToArray());

            return builder;
        }

        //Register automapper
        private ContainerBuilder RegisterAutoMappings(ContainerBuilder builder)
        {
            var assemblyNames = DependencyContext.Default.GetRuntimeAssemblyNames(string.Empty)
              .Where(a => a.FullName.StartsWith("CityComparison"));

            var assembliesTypes = assemblyNames
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf()
            .AutoActivate()
            .SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>()
                                        .CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            return builder;
        }

    }
}
