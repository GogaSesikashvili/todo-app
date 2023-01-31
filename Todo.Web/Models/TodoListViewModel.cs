using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Models
{
    /// <summary>
    /// Class for todo list view model.
    /// </summary>
    public class TodoListViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length should be 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Hide status.
        /// </summary>
        [Display(Name = "Hide?")]
        public bool Hide { get; set; }

        /// <summary>
        /// List of To Do Entries.
        /// </summary>
        public List<TodoEntryViewModel> ToDoEntries { get; set; } = new List<TodoEntryViewModel>();
    }
}