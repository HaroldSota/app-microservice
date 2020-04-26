using AppWeather.Api.Framework.Configuration.Bindings;
using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.ExternalServices.Google
{
    /// <inheritdoc cref="IGooglePlacesApiConfiguration"/>
    public sealed class GooglePlacesApiConfiguration : BindingConfiguration, IGooglePlacesApiConfiguration
    {
        /// <summary>
        ///     GooglePlacesApiConfiguration ctor.
        /// </summary>
        /// <param name="configuration"></param>
        public GooglePlacesApiConfiguration(IConfiguration configuration)
        : base(configuration, "GooglePlacesBinding")
        {
        }
    }
}