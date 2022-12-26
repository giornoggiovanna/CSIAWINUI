using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TimesUp.ViewModels
{
    public class AddTaskViewModel : ViewModelBase
    {
        private string _name = string.Empty;

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
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

        //public string Error => null!;

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        if (columnName == "Name")
        //        {
        //            // Validate property and return a string if there is an error
        //            if (string.IsNullOrEmpty(Name))
        //                return "Name is Required";
        //        }

        //        // If there's no error, null gets returned
        //        return null!;
        //    }
        //}
    }
}
