using Microsoft.EntityFrameworkCore;
using Todo.Data.DataAccess;
using Todo.Data.Domain;

namespace Todo.Data
{
    /// <summary>
    /// Class for Todo List repository.
    /// </summary>
    public class TodoListRepository : BaseRepository<TodoList, DbContext>, ITodoListRepository
    {
        public TodoListRepository(TodoAppDbContext context) : base(context)
        { }
    }
}