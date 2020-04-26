namespace AppWeather.Api.Framework.Configuration.Bindings
{
    public interface IBindingConfiguration
    {
        string Name { get; }
        string Endpoint { get; }

        string ApiKeyName { get; }

        string ApiKeyValue { get; }
        IBindingResourceConfiguration[] Resources { get; }
    }
}