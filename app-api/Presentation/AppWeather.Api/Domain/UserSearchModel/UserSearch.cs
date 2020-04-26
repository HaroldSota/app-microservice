using System;

namespace AppWeather.Api.Domain.UserSearchModel
{
    /// <summary>
    ///     Contains the user search data
    /// </summary>
    public sealed class UserSearch : BaseEntity
    {
        /// <summary>
        ///     UserSearch ctor, auto generated id
        /// </summary>
        public UserSearch(string userId, string cityName, float? temperature, int? humidity, DateTime searchTime)
            : this(Guid.NewGuid(), userId, cityName, temperature, humidity, searchTime)
        {
        }

        /// <summary>
        ///     UserSearch ctor, 
        /// </summary>
        private UserSearch(Guid id, string userId, string cityName, float? temperature, int? humidity, DateTime searchTime)
            : base(id)
        {
            UserId = userId;
            CityName = cityName;
            Temperature = temperature;
            Humidity = humidity;
            SearchTime = searchTime;
        }

        /// <summary>
        ///     User identifier
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        ///     Searched city name
        /// </summary>
        public string CityName { get; private set; }

        /// <summary>
        ///   Temperature at search time
        /// </summary>
        public float? Temperature { get; private set; }

        /// <summary>
        ///     Humidity  at search time
        /// </summary>
        public int? Humidity { get; private set; }

        /// <summary>
        ///     Time of search
        /// </summary>
        public DateTime SearchTime { get; private set; }
    }
}