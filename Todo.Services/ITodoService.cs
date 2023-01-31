using Todo.Data.Domain;

namespace Todo.Services
{
    /// <summary>
    /// Interface for todo services.
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Gets todo list. 
        /// </summary>
        /// <param name="hidden">
        /// 1. true returns hidden list 
        /// 2. false returns non-hiden list 
        /// 3. null returns all lists
        /// </param>
        /// <returns>Todo list.</returns>
        Task<List<TodoList>> GetTodoListsAsync(bool? hidden = null);

        /// <summary>
        /// Gets list by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>List.</returns>
        Task<TodoList> GetListByIdAsync(int id);

        /// <summary>
        /// Inserts list.
        /// </summary>
        /// <param name="list"></param>
        Task InsertListAsync(TodoList list);

        /// <summary>
        /// Copies list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        Task CopyListAsync(TodoList list);

        /// <summary>
        /// Updates list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        Task UpdateListAsync(TodoList list);

        /// <summary>
        /// Removes list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        Task RemoveListAsync(TodoList list);

        /// <summary>
        /// Gets entries to be reminded now.
        /// </summary>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetCurrentReminderEntriesAsync();

        /// <summary>
        /// Gets entries by list id.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <param name="hideCompleted">
        /// 1. True returns non-completed entries in list.
        /// 2. False returns all entries in list.
        /// </param>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetEntriesByListIdAsync(int listId, bool hideCompleted = false);

        /// <summary>
        /// Gets entries due today.
        /// </summary>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetEntriesDueTodayAsync();

        /// <summary>
        /// Gets entries with reminders.
        /// </summary>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetReminderEntriesAsync();

        /// <summary>
        /// Gets personal entries.
        /// </summary>
        /// <returns>Entries.</returns>
        Task<List<TodoEntry>> GetPersonalEntriesAsync();

        /// <summary>
        /// Inserts entry.
        /// </summary>
        /// <param name="entry">Entry.</param>
        Task InsertEntryAsync(TodoEntry entry);

        /// <summary>
        /// Gets Entry by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Entry.</returns>
        Task<TodoEntry> GetEntryByIdAsync(int id);

        /// <summary>
        /// Updates entry.
        /// </summary>
        /// <param name="entry">Entry.</param>
        Task UpdateEntryAsync(TodoEntry entry);

        /// <summary>
        /// Removes entry.
        /// </summary>
        /// <param name="entry">Entry.</param>
        Task RemoveEntryAsync(TodoEntry entry);
    }
}