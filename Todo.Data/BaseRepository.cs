using Microsoft.EntityFrameworkCore;

namespace Todo.Data
{
    public class BaseRepository<T, D> : IBaseRepository<T>
        where T : class
        where D : DbContext
    {
        protected readonly D dbContext;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(D dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Entities.</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Entity.</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        /// Inserts entity and saves.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates entity and saves.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dbSet.Update(entity);

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates entities and saves.
        /// </summary>
        /// <param name="entities">Entities.</param>
        public async Task UpdateAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            dbSet.UpdateRange(entities);

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes entity and saves.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task RemoveAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dbSet.Remove(entity);

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets table.
        /// </summary>
        public IQueryable<T> Table => dbSet;
    }
}