using Microsoft.EntityFrameworkCore;
using Todo.Data.Domain;

namespace Todo.Data.DataAccess
{
    /// <summary>
    /// TodoApp's DbContext class.
    /// </summary>
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<TodoEntry> TodoEntries { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
    }
}