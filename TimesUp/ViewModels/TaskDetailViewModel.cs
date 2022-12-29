using System;
using TimesUp.Database;

namespace TimesUp.ViewModels
{
    public class TaskDetailViewModel : ViewModelBase
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public int CurrentEffort { get; private set; }

        public int ExpectedEffort { get; private set; }

        public DateOnly? DueDate { get; private set; }

        public DateTimeOffset? CompletedDate { get; private set; } 

        public void LoadTask(Guid id)
        {
            var dbTask = DataAccess.GetTask(id);

            Id = dbTask.Id;
            Name = dbTask.Name;
            Description = dbTask.Description;
            CurrentEffort = dbTask.CurrentEffort;
            ExpectedEffort = dbTask.ExpectedEffort;
            DueDate = dbTask.DueDate;
            
        }
    }
}
