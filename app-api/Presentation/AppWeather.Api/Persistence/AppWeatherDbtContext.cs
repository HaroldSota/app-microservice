using AppWeather.Api.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace AppWeather.Api.Persistence
{
    public class AppWeatherDbtContext : DbContext, IDbContext
    {
        public AppWeatherDbtContext(DbContextOptions<AppWeatherDbtContext> options)
            : base(options)
        {
        }


        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class, new()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSearchData>().ToTable("UserSearch");
            modelBuilder.Entity<UserSearchData>().HasKey(t => t.Id);
            modelBuilder.Entity<UserSearchData>().Property(t => t.UserId).HasMaxLength(36).IsRequired();
            modelBuilder.Entity<UserSearchData>().Property(t => t.CityName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<UserSearchData>().Property(t => t.SearchTime).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
