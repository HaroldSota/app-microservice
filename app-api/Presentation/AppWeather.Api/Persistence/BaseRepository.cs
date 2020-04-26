using AppWeather.Api.Domain;
using AppWeather.Api.Framework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppWeather.Api.Persistence
{
    /// <inheritdoc/>
    public class BaseRepository<TEntity, TData> : IBaseRepository<TEntity, TData>
        where TEntity : BaseEntity
        where TData : class, IData, new()
    {
        private readonly IDbContext _dbContext;

        private DbSet<TData> _entities;

        /// <summary>
        ///     Base repository ctor.
        /// </summary>
        /// <param name="dbContext">The application database context object</param>
        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Query service for TEntity repository
        /// </summary>
        protected virtual DbSet<TData> Entities => _entities ??= _dbContext.Set<TData>();

        /// <summary>
        ///     Inserts a new row in the TEntity table 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                var data = Singleton<IMapper>.Instance.Map<TData>(entity);
                Entities.Add(data);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetErrorAndRollback(exception), exception);
            }
        }


        private string GetErrorAndRollback(DbUpdateException exception)
        {
            if (_dbContext is DbContext dbContext)
            {
                try
                {
                    dbContext.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                        .ToList()
                        .ForEach(entry => entry.State = EntityState.Unchanged);
                }
                catch (Exception ex)
                {
                    exception = new DbUpdateException(exception.ToString(), ex);
                }
            }

            _dbContext.SaveChanges();
            return exception.ToString();
        }
    }
}
