using System;
using System.Collections.ObjectModel;
using TimesUp.Database;

namespace TimesUp.ViewModels
{
    public class ToDoTasksViewModel
    {
        public ObservableCollection<ToDoTaskItemViewModel> Tasks { get; set; } = new();

        public ToDoTasksViewModel()
        {
            LoadTasks();
        }

        public void LoadTasks()
        {
            var dbTasks = DataAccess.GetToDoTasks();

            Tasks.Clear();

            foreach (var dbTask in dbTasks)
            {
                Tasks.Add(new ToDoTaskItemViewModel
                {
                    Id = dbTask.Id,
                    Name = dbTask.Name,
                    Description = dbTask.Description,
                    CurrentEffort = dbTask.CurrentEffort,
                    ExpectedEffort = dbTask.ExpectedEffort,
                    DueDate = dbTask.DueDate
                });
            }
        }
    }

    public class ToDoTaskItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ExpectedEffort { get; set; }
        public int CurrentEffort { get; set; }
        public int RemainingEffort { get => ExpectedEffort - CurrentEffort; }
        public DateOnly DueDate { get; set; }
    }
}
