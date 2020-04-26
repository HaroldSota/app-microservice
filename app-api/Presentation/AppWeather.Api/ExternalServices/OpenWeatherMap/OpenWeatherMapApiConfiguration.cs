using AppWeather.Api.Framework.Configuration.Bindings;
using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.ExternalServices.OpenWeatherMap
{
    /// <inheritdoc cref="IOpenWeatherMapConfiguration"/>
    public sealed class OpenWeatherMapApiConfiguration : BindingConfiguration, IOpenWeatherMapConfiguration
    {
        /// <summary>
        ///     OpenWeatherMapApiConfiguration ctor.
        /// </summary>
        /// <param name="configuration"></param>
        public OpenWeatherMapApiConfiguration(IConfiguration configuration)
            : base(configuration, "OpenWeatherMap")
        {
        }
    }
}
