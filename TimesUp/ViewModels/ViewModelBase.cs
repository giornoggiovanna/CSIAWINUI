using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimesUp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                OnPropertyChanged(propertyName, newValue);
            }
        }

        private void OnPropertyChanged(string propertyName, object? value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
