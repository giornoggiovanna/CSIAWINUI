using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using TimesUp.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;

namespace TimesUp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void SideBarMenu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
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
