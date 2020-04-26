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

        public string UserId { get; private set; }

        public string CityName { get; private set; }

        public float? Temperature { get; private set; }

        public int? Humidity { get; private set; }
        public DateTime SearchTime { get; private set; }
    }
}