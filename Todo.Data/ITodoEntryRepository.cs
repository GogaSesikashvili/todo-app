using Todo.Data.Domain;

namespace Todo.Data
{
    /// <summary>
    /// Interface for TodoEntryRepository.
    /// </summary>
    public interface ITodoEntryRepository : IBaseRepository<TodoEntry>
    {
        /// <summary>
        /// Gets entries by list id.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetByListIdAsync(int listId);
    }
}