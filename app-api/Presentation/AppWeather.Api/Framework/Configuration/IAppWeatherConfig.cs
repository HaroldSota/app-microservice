using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration
{
    /// <summary>
    ///     Meta data definition for the application configuration file AppWeatherConfig section
    /// </summary>
    public interface IAppWeatherConfig
    {
        /// <summary>
        ///     Determine if the app is called by automated testing
        /// </summary>
        bool IsTesting { get; }

        /// <summary>
        ///     User search cookie name
        /// </summary>
        string UserSearchCookie { get; }

        /// <summary>
        ///     Database connection string
        /// </summary>
        string DataConnectionString { get; }

        /// <summary>
        ///     List of external service bindings
        /// </summary>
        public IConfigurationSection Bindings { get; }
    }
}
