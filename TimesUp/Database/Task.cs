using System;

namespace TimesUp.Database
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public int ExpectedEffort { get; set; }
        public TimeSpan CurrentEffort { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
    }
}
