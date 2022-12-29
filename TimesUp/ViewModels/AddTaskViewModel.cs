using FluentValidation;
using System;
using TimesUp.Commands;
using TimesUp.Database;
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

        private DateTimeOffset? _dueDate = DateTime.Now.AddDays(1);
        
        public DateTimeOffset? DueDate { 
            get => _dueDate;
            set
            {
                SetValue(ref _dueDate, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        
        public int ExpectedEffort { 
            get => (_expectedEffortHours * 60) + _expectedEffortMinutes;
        }

        private int _expectedEffortHours = 0;

        public int ExpectedEffortHours {
            get => _expectedEffortHours;
            set
            {
                SetValue(ref _expectedEffortHours, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private int _expectedEffortMinutes = 0;

        public int ExpectedEffortMinutes
        {
            get => _expectedEffortMinutes;

            set
            {
                SetValue(ref _expectedEffortMinutes, value);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand AddCommand { get; }
        public ToDoTasksPage ToDoTasksPage { get; }

        private bool CanAddTask(object? arg)
        {
            var validationResult = _validator.Validate(this);
            return validationResult.IsValid;
        }

        private void AddTask(object? obj)
        {
            var newTask = new Task
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                DueDate = DateOnly.FromDateTime(DueDate!.Value.Date),
                ExpectedEffort = ExpectedEffort
            };

            DataAccess.AddTask(newTask);

            AddTaskDialog.Hide();
        }

        public void CloseDialog(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            AddTaskDialog.Hide();
        }
    }

    public class AddTaskValidator : AbstractValidator<AddTaskViewModel>
    {
        private const int MaximumExpectedEffort = 100 * 60; //100 hrs * 60 mins;

        public AddTaskValidator() 
        {
            RuleFor(task => task.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(task => task.Description)
                .MaximumLength(500);

            RuleFor(task => task.DueDate)
                .NotEmpty()
                .GreaterThan(DateTime.Now);

            RuleFor(task => task.ExpectedEffort)
                .GreaterThan(0)
                .LessThan(MaximumExpectedEffort)
                .NotEmpty();
        }
    }
}
