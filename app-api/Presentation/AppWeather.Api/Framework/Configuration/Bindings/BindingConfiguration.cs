using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration.Bindings
{
    public abstract class BindingConfiguration : BaseConfigurationProvider, IBindingConfiguration
    {
        public BindingConfiguration(IConfiguration configuration, string prefix)
            : base(configuration, prefix)
        {
        }

        private string _name = null;
        public string Name => _name ??= GetConfiguration<string>("Name");

        private string _endPoint = null;
        public string Endpoint => _endPoint ??= GetConfiguration<string>("Endpoint");

        private string _apiKeyName = null;
        public string ApiKeyName => _apiKeyName ??= GetConfiguration<string>("ApiKeyName");

        private string _apiKeyValue = null;
        public string ApiKeyValue => _apiKeyValue ??= GetConfiguration<string>("ApiKeyValue");

        private IBindingResourceConfiguration[] _resources;
        public IBindingResourceConfiguration[] Resources
            => _resources 
                ??= GetArrayConfiguration("Resources",
                    (config, prefix) => new BindingResourceConfiguration(config, prefix));
            
        
    }
}
