using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TimesUp.ViewModels;

namespace TimesUp.Pages
{
    public sealed partial class CompletedTasksPage : Page
    {
        public CompletedTasksPage() 
        {
            this.InitializeComponent();

            
        }

        public CompletedTasksViewModel ViewModel { get; set; }

        private void ListGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }

    
}
