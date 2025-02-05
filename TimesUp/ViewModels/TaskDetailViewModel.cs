﻿using Microsoft.UI.Xaml;
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

        private Guid updateId;

        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        private TimeSpan _currentEffort;

        private TimeSpan privateCurrentEffort;

        public TimeSpan CurrentEffort { 
            get => _currentEffort;
            private set 
            { 
                SetValue(ref _currentEffort, value);
                OnPropertyChanged("CurrentEffort");
            }
        }

        public int ExpectedEffort { get; private set; }

        public DateOnly? DueDate { get; private set; }

        public string ElapsedTimeFormatted { get => ElapsedTime.ToString(@"hh\:mm\:ss"); }

        public string CurrentEffortFormated { get => CurrentEffort.ToString(@"hh\:mm\:ss"); }

        private TimeSpan _elapsedTime = TimeSpan.Zero;

        private TimeSpan placeholder = TimeSpan.Zero;

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

            CurrentEffort = CurrentEffort.Add(ElapsedTime);

            if ((double)ExpectedEffort <= CurrentEffort.TotalMinutes) 
            {
            
                _timer.Stop();
                OnPropertyChanged("CanStartTimer");
                OnPropertyChanged("CanStopTimer");

                CurrentEffort = privateCurrentEffort;
                DataAccess.UpdateCurrentEffort(updateId, CurrentEffort);
            }
        }

        public void StartTimer()
        {
            if (ExpectedEffort > CurrentEffort.TotalMinutes)
            {

                _startedTime = DateTimeOffset.Now;
                _timer.Start();

                OnPropertyChanged("CanStartTimer");
                OnPropertyChanged("CanStopTimer");

            }
            
            

        }

        public void StopTimer()
        {
            
            if (_timer.IsEnabled)
            {
                CurrentEffort = privateCurrentEffort;
                _timer.Stop();

                OnPropertyChanged("CanStartTimer");
                OnPropertyChanged("CanStopTimer");

                
                ElapsedTime = TimeSpan.Zero;

            }

            DataAccess.UpdateCurrentEffort(updateId, CurrentEffort);

        }

        public void LoadTask(Guid id)
        {
            var dbTask = DataAccess.GetTask(id);

            updateId = id;

            Id = dbTask.Id;
            Name = dbTask.Name;
            Description = dbTask.Description;
            CurrentEffort = dbTask.CurrentEffort;
            ExpectedEffort = dbTask.ExpectedEffort;
            DueDate = dbTask.DueDate;
        }

        public void OnCompleteBtnClick()
        {
            DataAccess.AddCompletionDate(updateId);
        }


    }
}
