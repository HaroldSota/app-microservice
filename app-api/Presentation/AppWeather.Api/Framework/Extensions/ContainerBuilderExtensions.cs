using AppWeather.Api.Framework.Configuration;
using AppWeather.Api.Domain.UserSearchModel;
using AppWeather.Api.ExternalServices.Google;
using AppWeather.Api.ExternalServices.OpenWeatherMap;
using AppWeather.Api.Persistence;
using AppWeather.Api.Persistence.Model;
using AppWeather.Api.Persistence.Model.UserSearch;
using Autofac;
using Microsoft.EntityFrameworkCore;

namespace AppWeather.Api.Framework.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterApiDependencies(this ContainerBuilder builder, IAppWeatherConfig appWeatherConfig)
        {
            builder.RegisterInstance(appWeatherConfig).As<IAppWeatherConfig>().SingleInstance();
        }

        public static void RegisterExternalServicesDependencies(this ContainerBuilder builder, IAppWeatherConfig appWeatherConfig)
        {

            builder
                 .RegisterType<GooglePlacesApiService>()
                 .As<IGooglePlacesApiService>()
                 .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new GooglePlacesApiConfiguration(appWeatherConfig.Bindings))
                .As<IGooglePlacesApiConfiguration>()
                .SingleInstance();

            builder
                 .RegisterType<OpenWeatherMapApiService>()
                 .As<IOpenWeatherMapApiService>()
                 .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new OpenWeatherMapApiConfiguration(appWeatherConfig.Bindings))
                .As<IOpenWeatherMapConfiguration>()
                .SingleInstance();
        }

        public static void RegisterPersistenceDependencies(this ContainerBuilder builder)
        {
            builder
                .Register(context => new AppWeatherDbtContext(context.Resolve<DbContextOptions<AppWeatherDbtContext>>()))
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            builder
                 .RegisterGeneric(typeof(BaseRepository<,>))
                 .As(typeof(IBaseRepository<,>));

            builder.RegisterType<UserSearchRepository>().As<IUserSearchRepository>();

        }
    }
}
