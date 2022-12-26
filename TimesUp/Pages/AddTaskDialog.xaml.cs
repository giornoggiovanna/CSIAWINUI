using Microsoft.UI.Xaml.Controls;
using TimesUp.ViewModels;

namespace TimesUp.Pages
{
    public sealed partial class AddTaskDialog : ContentDialog
    {
        public AddTaskDialog()
        {
            this.InitializeComponent();

            ViewModel = new AddTaskViewModel();
        }

        public AddTaskViewModel ViewModel { get; }
    }
}
