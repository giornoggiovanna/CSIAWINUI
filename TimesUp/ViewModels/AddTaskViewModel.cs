using FluentValidation;
using System;
using TimesUp.Commands;
using TimesUp.Pages;

namespace TimesUp.ViewModels
{
    public class AddTaskViewModel : ViewModelBase
    {
        private readonly AddTaskValidator _validator = new();

        public AddTaskDialog AddTaskDialog { get; }

        public AddTaskViewModel(AddTaskDialog addTaskDialog)
        {
            AddCommand = new DelegateCommand(AddTask, CanAddTask);
            AddTaskDialog = addTaskDialog;
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                SetValue(ref _name, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _description = string.Empty;

        public string Description
        {
            get => _description;
            set
            {
                SetValue(ref _description, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTimeOffset _dueDate = DateTime.Now.AddDays(1);

        public DateTimeOffset DueDate { 
            get => _dueDate;
            set
            {
                SetValue(ref _dueDate, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand AddCommand { get; }

        private bool CanAddTask(object? arg)
        {
            var validationResult = _validator.Validate(this);
            return validationResult.IsValid;
        }

        private void AddTask(object? obj)
        {
            throw new NotImplementedException();
        }

        public void CloseDialog(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            AddTaskDialog.Hide();
        }

    }

    public class AddTaskValidator : AbstractValidator<AddTaskViewModel>
    {
        public AddTaskValidator() 
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(task => task.Description)
                .MaximumLength(500);

            RuleFor(task => task.DueDate)
                .GreaterThan(DateTime.Now);
        }
    }
}
