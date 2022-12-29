using System.Collections.ObjectModel;
using TimesUp.Database;

namespace TimesUp.ViewModels
{
    public class ToDoViewModel
    {
        public ObservableCollection<ToDoItemViewModel> Tasks { get; set; } = new();

        public ToDoViewModel()
        {
            LoadTasks();
        }

        public void LoadTasks()
        {
            var dbTasks = DataAccess.GetToDoTasks();

            Tasks.Clear();

            foreach (var dbTask in dbTasks)
            {
                Tasks.Add(new ToDoItemViewModel
                {
                    Name = dbTask.Name,
                    Description = dbTask.Description,
                    CurrentEffort = dbTask.CurrentEffort,
                    ExpectedEffort = dbTask.ExpectedEffort
                });
            }
        }
    }

    public class ToDoItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ExpectedEffort { get; set; }
        public int CurrentEffort { get; set; }
        public int RemainingEffort { get => ExpectedEffort - CurrentEffort; }
    }
}
