using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using TimesUp.ViewModels;

namespace TimesUp.Pages
{
    public sealed partial class ToDoTasksPage : Page
    {
        public ToDoTasksPage()
        {
            this.InitializeComponent();

            ViewModel = new ToDoTasksViewModel();
        }

        public ToDoTasksViewModel ViewModel { get; }
        
        private async void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTaskDialog
            {
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();

            ViewModel.LoadTasks();
        }

        private void ToDoListGridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = (GridView)sender;
            var selectedTask = (ToDoTaskItemViewModel)grid.SelectedItem;

            Frame.Navigate(typeof(TaskDetailPage), selectedTask.Id);
        }
    }
}
