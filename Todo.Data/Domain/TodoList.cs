using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Domain
{
    /// <summary>
    /// Class for todo list.
    /// </summary>
    public class TodoList
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required(ErrorMessage = "Todo list's title should not be empty.")]
        [MaxLength(100, ErrorMessage = "Maximum length should be 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Hide status.
        /// </summary>
        public bool Hide { get; set; }

        /// <summary>
        /// List of To Do Entries.
        /// </summary>
        public List<TodoEntry> ToDoEntries { get; set; } = new List<TodoEntry>();
    }
}