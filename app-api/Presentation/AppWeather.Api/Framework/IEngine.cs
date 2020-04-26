using System;

namespace AppWeather.Api.Framework
{
    /// <summary>
    ///     Classes implementing this interface can serve as a portal for the various services composing the AppWeather engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        ///    Sets service provider
        /// </summary>
        void SetServiceProvider(IServiceProvider serviceProvider);

        /// <summary>
        ///     Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        T Resolve<T>() where T : class;
    }
}
