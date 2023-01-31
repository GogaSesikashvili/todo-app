using Todo.Data.Domain;

namespace Todo.Data
{
    /// <summary>
    /// Interface for TodoListRepository.
    /// </summary>
    public interface ITodoListRepository : IBaseRepository<TodoList>
    { }
}