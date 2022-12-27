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

        private int _expectedEffort;
        
        public int ExpectedEffort { 
            get => (((_expectedEffortHours * 60) * 60) * 1000) + ((_expectedEffortMinutes * 60) * 1000);
            set 
            { 
                SetValue(ref _expectedEffort, value);
            } 
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
            //ToDoTasksPage toDoTasksPage = new();

            //tasks.Add(new TaskItem() { TName = Name, TDescription = Description, TDueDate = DueDate, TExpectedEffort = ExpectedEffort , TCurrentEffort = 0, TRemainingEffort = ExpectedEffort - 0 });

            //toDoTasksPage.ToDoListGridview.ItemsSource = tasks;
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

            RuleFor(task => task.ExpectedEffort)
                .NotEmpty();
        }
    }
}
