using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using TimesUp.ViewModels;

namespace TimesUp.Pages
{
    public sealed partial class TaskDetailPage : Page
    {
        public TaskDetailPage()
        {
            this.InitializeComponent();
        }

        public TaskDetailViewModel ViewModel { get; set; } = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var taskId = (Guid)e.Parameter;

            ViewModel.LoadTask(taskId);

            base.OnNavigatedTo(e);
        }
    }
}
