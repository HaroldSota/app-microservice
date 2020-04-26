using System;

namespace AppWeather.Api.Persistence
{
    /// <summary>
    ///  The classes implementing IData interface are marked as Persistence Model Entity
    /// </summary>
    public interface IData 
    {
        /// <summary>
        /// The table Id
        /// </summary>
        Guid Id { get; }
    }
}
