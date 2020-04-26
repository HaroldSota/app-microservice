using AppWeather.Api.Framework.Extensions;
using AppWeather.Api.Framework.Configuration;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppWeather.Api.Framework;

namespace AppWeather.Api
{
    public class Startup
    {
        private readonly IAppWeatherConfig _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = new AppWeatherConfig(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceServices(_configuration);
            services.AddApiServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterExternalServicesDependencies(_configuration);
            builder.RegisterPersistenceDependencies();
            builder.RegisterApiDependencies(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiRequestPipeline();
            app.UseEngineContext();
        }
    }
}
