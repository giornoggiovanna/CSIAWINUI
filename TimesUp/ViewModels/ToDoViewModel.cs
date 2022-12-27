using System.Collections.Generic;
using System;
using TimesUp.Pages;
using System.Collections.ObjectModel;

namespace TimesUp.ViewModels
{
    public class ToDoViewModel
    {
        public ObservableCollection<ToDoItemViewModel> Tasks { get; set; } = new();

        public ToDoViewModel()
        {
            Tasks.Add(new ToDoItemViewModel { Name = "slajfjslef", Description = "sjlfjaldf", ExpectedEffort = 50, CurrentEffort = 20 });
            Tasks.Add(new ToDoItemViewModel { Name = "slajfjslef", Description = "sjlfjaldf", ExpectedEffort = 60, CurrentEffort = 20 });
            Tasks.Add(new ToDoItemViewModel { Name = "slajfjslef", Description = "sjlfjaldf", ExpectedEffort = 50, CurrentEffort = 30 });
            Tasks.Add(new ToDoItemViewModel { Name = "slajfjslef", Description = "sjlfjaldf", ExpectedEffort = 10, CurrentEffort = 10 });
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
