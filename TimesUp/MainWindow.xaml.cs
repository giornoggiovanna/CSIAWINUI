using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using TimesUp.Pages;

namespace TimesUp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void MainNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == false)
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                DoNavigate(sender, item);
            }
        }

        private void DoNavigate(NavigationView navigationView, NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "ToDoTasks":
                    navigationView.Header = item.Content;
                    ContentFrame.Navigate(typeof(ToDoTasksPage));
                    break;
                case "CompletedTasks":
                    navigationView.Header = item.Content;
                    ContentFrame.Navigate(typeof(CompletedTasksPage));
                    break;
            }
        }
    }
}
