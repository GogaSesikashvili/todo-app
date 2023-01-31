using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Domain
{
    /// <summary>
    /// Class for todo entry.
    /// </summary>
    public class TodoEntry
    {
        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        [Required(ErrorMessage = "Todo entry's title should not be empty.")]
        [MaxLength(100, ErrorMessage = "Maximum length should be 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [MaxLength(600, ErrorMessage = "Maximum length should be 600 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Additional notes.
        /// </summary>
        public string AdditionalNotes { get; set; }

        /// <summary>
        /// Label.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Due date.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Reminder.
        /// </summary>
        public DateTime? Reminder { get; set; }

        /// <summary>
        /// True if it was reminded, otherwise false.
        /// </summary>
        public bool IsReminded { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Completion status.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Todo list's id.
        /// </summary>
        [Required(ErrorMessage = "Todo list's id should not be empty.")]
        public int TodoListId { get; set; }
    }

    public enum Label
    {
        General = 0,
        Personal = 1,
        Work = 2,
    }

    public enum Status
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
    }
}