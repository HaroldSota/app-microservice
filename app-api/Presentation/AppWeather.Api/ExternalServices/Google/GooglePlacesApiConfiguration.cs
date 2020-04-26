using AppWeather.Api.Framework.Configuration.Bindings;
using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.ExternalServices.Google
{
    public sealed class GooglePlacesApiConfiguration : BindingConfiguration, IGooglePlacesApiConfiguration
    {
        public GooglePlacesApiConfiguration(IConfiguration configuration)
        : base(configuration, "GooglePlacesBinding")
        {
        }
    }
}