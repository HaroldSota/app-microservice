using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration.Bindings
{
    /// <inheritdoc cref="IBindingConfiguration" />
    public abstract class BindingConfiguration : BaseConfigurationProvider, IBindingConfiguration
    {
        /// <summary>
        ///     BindingConfiguration ctor.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="prefix"></param>
        public BindingConfiguration(IConfiguration configuration, string prefix)
            : base(configuration, prefix)
        {
        }

        private string _name;

        /// <inheritdoc />
        public string Name => _name ??= GetConfiguration<string>("Name");


        private string _endPoint;
        /// <inheritdoc />
        public string Endpoint => _endPoint ??= GetConfiguration<string>("Endpoint");

        private string _apiKeyName;
        /// <inheritdoc />
        public string ApiKeyName => _apiKeyName ??= GetConfiguration<string>("ApiKeyName");

        private string _apiKeyValue;
        /// <inheritdoc />
        public string ApiKeyValue => _apiKeyValue ??= GetConfiguration<string>("ApiKeyValue");

        private IBindingResourceConfiguration[] _resources;
        /// <inheritdoc />
        public IBindingResourceConfiguration[] Resources
            => _resources 
                ??= GetArrayConfiguration("Resources",
                    (config, prefix) => new BindingResourceConfiguration(config, prefix));
    }
}