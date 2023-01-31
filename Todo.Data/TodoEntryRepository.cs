using Microsoft.EntityFrameworkCore;
using Todo.Data.DataAccess;
using Todo.Data.Domain;

namespace Todo.Data
{
    /// <summary>
    /// Class for Todo Entry repository.
    /// </summary>
    public class TodoEntryRepository : BaseRepository<TodoEntry, DbContext>, ITodoEntryRepository
    {
        public TodoEntryRepository(TodoAppDbContext context) : base(context)
        { }

        /// <summary>
        /// Gets entries by list id.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetByListIdAsync(int listId)
        {
            return await (
                from te in dbSet
                where te.TodoListId == listId
                select te
                ).ToListAsync();
        }
    }
}