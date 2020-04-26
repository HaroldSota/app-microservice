using AppWeather.Api.Framework.Configuration;
using AppWeather.Api.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AppWeather.Api.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddLogging();
            services.AddControllers();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                        builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "AppWeather API", Version = "v1" });
                cfg.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);

            });

        }

        public static void AddPersistenceServices(this IServiceCollection services, IAppWeatherConfig appConfig)
        {
            services.AddAutoMapper();

            services.AddDbContext<AppWeatherDbtContext>(optionsBuilder =>
            {

                if (appConfig.IsTesting)
                    optionsBuilder.UseLazyLoadingProxies()
                        .UseInMemoryDatabase("AppWeatherTestDb");
                else
                    // production Db
                    optionsBuilder.UseLazyLoadingProxies()
                        .UseSqlServer(appConfig.DataConnectionString,
                            options => { options.MigrationsAssembly("AppWeather.Api"); });
            });


        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var instances = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(type => typeof(Profile).IsAssignableFrom(type))
                                    .Select(mapperConfiguration => (Profile)Activator.CreateInstance(mapperConfiguration));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            //register
            Singleton<IMapper>.Instance = config.CreateMapper();
        }

    }
}
