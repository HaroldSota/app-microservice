namespace AppWeather.Api.Framework.Configuration.Bindings
{
    /// <summary>
    ///     BindingConfiguration
    /// </summary>
    public interface IBindingConfiguration
    {
        /// <summary>
        ///     Binding configuration name
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The resource base uri
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        ///     The resource api key name
        /// </summary>
        string ApiKeyName { get; }
        /// <summary>
        ///     The resource api key value
        /// </summary>
        string ApiKeyValue { get; }

        /// <summary>
        ///     The resources list that will be used by the application
        /// </summary>
        IBindingResourceConfiguration[] Resources { get; }
    }
}