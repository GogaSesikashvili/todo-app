using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Models
{
    /// <summary>
    /// Class for todo entry view model.
    /// </summary>
    public class TodoEntryViewModel
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
        /// Description.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [MaxLength(600, ErrorMessage = "Maximum length should be 600 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Additional notes.
        /// </summary>
        [Display(Name = "Additional Notes")]
        [DataType(DataType.MultilineText)]
        public string AdditionalNotes { get; set; }

        /// <summary>
        /// Label.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Due date.
        /// </summary>
        [Display(Name = "Due date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Reminder.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime? Reminder { get; set; }

        /// <summary>
        /// True if it was reminded, otherwise false.
        /// </summary>
        public bool IsReminded { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Todo list's id.
        /// </summary>
        [Required(ErrorMessage = "Completion status is required.")]
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
        [Display(Name = "Not Started")]
        NotStarted = 0,

        [Display(Name = "In Progress")]
        InProgress = 1,

        Completed = 2,
    }
}