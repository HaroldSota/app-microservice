using AppWeather.Api.Framework.Configuration.Bindings;
using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.ExternalServices.OpenWeatherMap
{
    public sealed class OpenWeatherMapApiConfiguration : BindingConfiguration, IOpenWeatherMapConfiguration
    {
        public OpenWeatherMapApiConfiguration(IConfiguration configuration)
            : base(configuration, "OpenWeatherMap")
        {
        }
    }
}
