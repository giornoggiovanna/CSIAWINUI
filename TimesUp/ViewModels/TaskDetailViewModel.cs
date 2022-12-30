using Microsoft.UI.Xaml;
using System;
using TimesUp.Database;

namespace TimesUp.ViewModels
{
    public class TaskDetailViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _timer = new();

        private DateTimeOffset? _startedTime;

        public TaskDetailViewModel()
        {
            _timer.Tick += OnTimerTick;
        }
        
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public int CurrentEffort { get; private set; }

        public int ExpectedEffort { get; private set; }

        public DateOnly? DueDate { get; private set; }

        public string ElapsedTimeFormatted { get => _elapsedTime.ToString(@"hh\:mm\:ss"); }

        private TimeSpan _elapsedTime = TimeSpan.Zero;

        public TimeSpan ElapsedTime { 
            get => _elapsedTime;
            private set 
            {
                SetValue(ref _elapsedTime, value);
                OnPropertyChanged("ElapsedTimeFormatted");
            }
        }

        public bool CanStartTimer { get => !_timer.IsEnabled; }

        public bool CanStopTimer { get => _timer.IsEnabled; }

        private void OnTimerTick(object? sender, object e)
        {
            ElapsedTime = DateTimeOffset.Now - _startedTime.Value;
        }

        public void StartTimer()
        {
            _startedTime = DateTimeOffset.Now;
            _timer.Start();

            OnPropertyChanged("CanStartTimer");
            OnPropertyChanged("CanStopTimer");
        }

        public void StopTimer()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();

                OnPropertyChanged("CanStartTimer");
                OnPropertyChanged("CanStopTimer");

                ElapsedTime = TimeSpan.Zero;
            }
        }

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
