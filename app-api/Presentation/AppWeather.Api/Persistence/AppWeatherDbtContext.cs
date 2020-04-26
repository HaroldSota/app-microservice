using AppWeather.Api.Persistence.Model;
using AppWeather.Api.Persistence.Model.UserSearch;
using Microsoft.EntityFrameworkCore;

namespace AppWeather.Api.Persistence
{
    /// <inheritdoc cref="IDbContext" />
    public class AppWeatherDbtContext : DbContext, IDbContext
    {
        /// <summary>
        ///     AppWeatherDbtContext ctor.
        /// </summary>
        /// <param name="options"></param>
        public AppWeatherDbtContext(DbContextOptions<AppWeatherDbtContext> options)
            : base(options)
        {
        }

        /// <inheritdoc cref="IDbContext" />
        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class, new()
        {
            return base.Set<TEntity>();
        }


        /// <summary>
        ///     Configure the model that was discovered by convention from the entity types
        ///     exposed in <see cref="DbSet{TEntity}" /> properties on your derived context. The resulting model may be cached
        ///     and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSearchData>(entity =>
            {
                entity.ToTable("UserSearch");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.UserId).HasMaxLength(36).IsRequired();
                entity.Property(t => t.CityName).HasMaxLength(50).IsRequired();
                entity.Property(t => t.SearchTime).IsRequired();
            });
           

            base.OnModelCreating(modelBuilder);
        }
    }
}
