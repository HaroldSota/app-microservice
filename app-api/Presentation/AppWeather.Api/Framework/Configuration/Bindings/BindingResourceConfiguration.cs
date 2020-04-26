using Microsoft.Extensions.Configuration;

namespace AppWeather.Api.Framework.Configuration.Bindings
{
    public class BindingResourceConfiguration : BaseConfigurationProvider, IBindingResourceConfiguration
    {
        public BindingResourceConfiguration(IConfiguration configuration, string prefix)
                : base(configuration, prefix)
        {
        }

        private string _name = null;
        public string Name => _name ??= GetConfiguration<string>("Name");


        public int _timeout = 0;
        public int Timeout => _timeout = _timeout > 0
            ? _timeout
            : int.Parse(GetConfiguration<string>("Timeout"));


        private string _contentType = null;
        public string ContentType => _contentType ??= GetConfiguration<string>("ContentType");


        private string _resource = null;
        public string Location => _resource ??= GetConfiguration<string>("Location");
    }
}