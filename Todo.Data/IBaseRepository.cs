namespace Todo.Data
{
    public interface IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Entities.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Inserts entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Updates entities and saves.
        /// </summary>
        /// <param name="entities">Entities.</param>
        Task UpdateAsync(List<T> entities);

        /// <summary>
        /// Removes entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Gets table.
        /// </summary>
        IQueryable<T> Table { get; }
    }
}