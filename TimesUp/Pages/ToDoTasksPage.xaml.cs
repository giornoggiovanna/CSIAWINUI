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

            ViewModel = new ToDoViewModel();
        }

        public ToDoViewModel ViewModel { get; }
        
        private async void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTaskDialog();
            dialog.XamlRoot = this.XamlRoot;

            var result = await dialog.ShowAsync();
        }

    }
}
