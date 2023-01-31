using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Domain;

namespace Todo.Services
{
    /// <summary>
    /// Class for todo services.
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly ITodoEntryRepository entryRepo;
        private readonly ITodoListRepository listRepo;

        public TodoService(ITodoEntryRepository entryRepo, ITodoListRepository listRepo)
        {
            this.entryRepo = entryRepo;
            this.listRepo = listRepo;
        }

        /// <summary>
        /// Gets todo list. 
        /// </summary>
        /// <param name="hidden">
        /// 1. True returns hidden list
        /// 2. False returns non-hiden list
        /// 3. Null returns all lists
        /// </param>
        /// <returns>Todo list.</returns>
        public async Task<List<TodoList>> GetTodoListsAsync(bool? hidden = null)
        {
            var query = listRepo.Table;

            if (hidden.HasValue)
            {
                query = query.Where(list => list.Hide == hidden.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets list by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>List.</returns>
        public async Task<TodoList> GetListByIdAsync(int id)
        {
            return await listRepo.GetByIdAsync(id);
        }

        /// <summary>
        /// Inserts list.
        /// </summary>
        /// <param name="list"></param>
        public async Task InsertListAsync(TodoList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            await listRepo.InsertAsync(list);
        }

        /// <summary>
        /// Copies list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        public async Task CopyListAsync(TodoList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var copiedList = new TodoList
            {
                Title = list.Title,
                Hide = list.Hide,
            };

            await listRepo.InsertAsync(copiedList);

            var entries = await entryRepo.GetByListIdAsync(list.Id);

            foreach (var entry in entries)
            {
                var copiedEntry = new TodoEntry
                {
                    Title = entry.Title,
                    Description = entry.Description,
                    AdditionalNotes = entry.AdditionalNotes,
                    Label = entry.Label,
                    DueDate = entry.DueDate,
                    CreationDate = entry.CreationDate,
                    Reminder = entry.Reminder,
                    IsReminded = entry.IsReminded,
                    Status = entry.Status
                };

                copiedList.ToDoEntries.Add(copiedEntry);
            }

            await listRepo.UpdateAsync(copiedList);
        }

        /// <summary>
        /// Updates list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        public async Task UpdateListAsync(TodoList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            await listRepo.UpdateAsync(list);
        }

        /// <summary>
        /// Removes list and saves.
        /// </summary>
        /// <param name="list">List.</param>
        public async Task RemoveListAsync(TodoList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            await listRepo.RemoveAsync(list);
        }

        /// <summary>
        /// Gets entries to be reminded now.
        /// </summary>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetCurrentReminderEntriesAsync()
        {
            var entries = await entryRepo.Table
                .Where(entry => entry.Reminder != null && entry.Reminder <= DateTime.Now && !entry.IsReminded).ToListAsync();

            var remindEntries = new List<TodoEntry>();

            foreach (var entry in entries)
            {
                if (entry.Reminder <= DateTime.Now)
                {
                    remindEntries.Add(entry);

                    entry.IsReminded = true;
                }
            }

            await entryRepo.UpdateAsync(remindEntries);

            return remindEntries;
        }

        /// <summary>
        /// Gets entries by list id.
        /// </summary>
        /// <param name="listId">List id.</param>
        /// <param name="hideCompleted">
        /// 1. True returns non-completed entries in list.
        /// 2. False returns all entries in list.
        /// </param>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetEntriesByListIdAsync(int listId, bool hideCompleted = false)
        {
            var entries = await entryRepo.GetByListIdAsync(listId);

            if (hideCompleted)
            {
                entries = entries.Where(entry => entry.Status != Status.Completed).ToList();
            }

            return entries;
        }

        /// <summary>
        /// Gets entries due today.
        /// </summary>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetEntriesDueTodayAsync()
        {
            return await entryRepo.Table
                .Where(entry => entry.DueDate.Value.Date == DateTime.Now.Date).ToListAsync();
        }

        /// <summary>
        /// Gets entries with reminders.
        /// </summary>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetReminderEntriesAsync()
        {
            return await entryRepo.Table
                .Where(entry => entry.Reminder != null).ToListAsync();
        }

        /// <summary>
        /// Gets personal entries.
        /// </summary>
        /// <returns>Entries.</returns>
        public async Task<List<TodoEntry>> GetPersonalEntriesAsync()
        {
            return await entryRepo.Table
                .Where(entry => entry.Label == Label.Personal).ToListAsync();
        }

        /// <summary>
        /// Inserts entry and saves.
        /// </summary>
        /// <param name="entry">Entry.</param>
        public async Task InsertEntryAsync(TodoEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            await entryRepo.InsertAsync(entry);
        }

        /// <summary>
        /// Gets Entry by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Entry.</returns>
        public async Task<TodoEntry> GetEntryByIdAsync(int id)
        {
            return await entryRepo.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates entry and saves.
        /// </summary>
        /// <param name="entry">Entry.</param>
        public async Task UpdateEntryAsync(TodoEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            await entryRepo.UpdateAsync(entry);
        }

        /// <summary>
        /// Removes entry and saves.
        /// </summary>
        /// <param name="entry">Entry.</param>
        public async Task RemoveEntryAsync(TodoEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            await entryRepo.RemoveAsync(entry);
        }
    }
}