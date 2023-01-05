using System;
using System.Collections.ObjectModel;
using TimesUp.Database;

namespace TimesUp.ViewModels
{
    public class CompletedTasksViewModel
    {
        public ObservableCollection<CompletedTaskItemViewModel> CompletedTasks { get; set; } = new();

        public CompletedTasksViewModel()
        {
            LoadCompletedTasks();
        }

        private void LoadCompletedTasks()
        {
            var dbTasks = DataAccess.GetCompletedTasks();
            CompletedTasks.Clear();

            foreach (var completedTask in dbTasks)
            {
                CompletedTasks.Add(new CompletedTaskItemViewModel 
                { 
                    Id = completedTask.Id, 
                    Name = completedTask.Name, 
                    Description= completedTask.Description,
                    CurrentEffort = completedTask.CurrentEffort,
                    ExpectedEffort= completedTask.ExpectedEffort,
                    DueDate= completedTask.DueDate,
                    CompletedDate= completedTask.DueDate
                });
               
            }
        }
    }

    public class CompletedTaskItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ExpectedEffort { get; set; }
        public TimeSpan CurrentEffort { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly CompletedDate { get; set; }
    }
}
