using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppWeather.Api.Persistence.Model.UserSearch
{
    /// <summary>
    ///     UserSearchData persistence entity
    /// </summary>
    [Table("UserSearch")]
    public class UserSearchData : IData
    {
        /// <summary>
        ///     The entity identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     Searched city name
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        ///   Temperature at search time
        /// </summary>
        public float? Temperature { get; set; }

        /// <summary>
        ///     Humidity  at search time
        /// </summary>
        public int? Humidity { get; set; }

        /// <summary>
        ///     Time of search
        /// </summary>
        public DateTime SearchTime { get; set; }
    }
}