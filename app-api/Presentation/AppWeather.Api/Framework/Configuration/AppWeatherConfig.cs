using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration
{
    /// <inheritdoc cref="IAppWeatherConfig" />
    public class AppWeatherConfig : BaseConfigurationProvider, IAppWeatherConfig
    {
        /// <summary>
        ///     AppWeatherConfig ctor.
        /// </summary>
        /// <param name="configuration"></param>
        public AppWeatherConfig(IConfiguration configuration)
            : base(configuration, "AppWeatherConfig")
        {
        }

        /// <inheritdoc />
        public bool IsTesting => base.GetConfiguration<bool>("IsTesting");

        private string _userSearchCookie;

        /// <inheritdoc />
        public string UserSearchCookie => _userSearchCookie ??= GetConfiguration<string>("UserSearchCookie");


        private string _dataConnectionString;

        /// <inheritdoc />
        public string DataConnectionString => _dataConnectionString ??= GetConfiguration<string>("DataConnectionString");


        private IConfigurationSection _bindings;

        /// <inheritdoc />
        public IConfigurationSection Bindings => _bindings ??= GetSection("Bindings");
    }
}
