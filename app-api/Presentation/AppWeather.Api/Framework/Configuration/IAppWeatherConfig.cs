using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration
{
    public interface IAppWeatherConfig
    {
        bool IsTesting { get; }
        string UserSearchCookie { get; }
        string DataConnectionString { get; }
        public IConfigurationSection Bindings { get; }
    }
}
