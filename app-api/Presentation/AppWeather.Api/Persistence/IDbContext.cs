using Microsoft.EntityFrameworkCore;

namespace AppWeather.Api.Persistence
{
    /// <summary>
    ///     Represents DBContext definition
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        ///     Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class, new();

        /// <summary>
        ///     Saves all changes made in this context to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        int SaveChanges();
    }
}