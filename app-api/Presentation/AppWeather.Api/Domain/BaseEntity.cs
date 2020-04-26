using System;

namespace AppWeather.Api.Domain
{
    /// <summary>
    ///     Base class for domain entities
    /// </summary>
    public abstract class BaseEntity
    {

        /// <summary>
        ///     Domain base entity ctor.
        /// </summary>
        /// <param name="id">The entity identifier</param>
        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets or sets the entity identifier
        /// </summary>
        public Guid Id { get; private set; }
    }
}