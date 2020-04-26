namespace AppWeather.Api.Framework.Configuration.Bindings
{
    public interface IBindingResourceConfiguration
    {
        string Name { get; }
        int Timeout { get; }
        string ContentType { get; }
        string Location { get; }
    }
}