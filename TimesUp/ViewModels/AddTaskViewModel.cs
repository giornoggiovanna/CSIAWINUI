using System;
using System.ComponentModel.DataAnnotations;

namespace TimesUp.ViewModels
{
    public class AddTaskViewModel : ViewModelBase
    {
        private string _name = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name must be 100 characters or less.")]
        public string Name
        {
            get => _name;
            set
            {
                SetValue(ref _name, value);
            }
        }

        private string _description = string.Empty;

        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description
        {
            get => _description;
            set
            {
                SetValue(ref _description, value);
            }
        }

        private DateTimeOffset _dueDate = DateTime.Now.AddDays(1);

        public DateTimeOffset DueDate { 
            get => _dueDate;
            set
            {
                SetValue(ref _dueDate, value);
            }
        }
    }
}
