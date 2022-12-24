using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace TimesUp.Pages
{
    public sealed partial class CompletedTasksPage : Page
    {
        public CompletedTasksPage() 
        {
            this.InitializeComponent();

            var tempList = CompletedTaskItem.GetItems();
            ObservableCollection<CompletedTaskItem> tasks = new ObservableCollection<CompletedTaskItem>(tempList);
            ListGridView.ItemsSource= tasks;
        }

        private void ListGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        
    }

    public class CompletedTaskItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ExpectedEffort { get; set; }
        public int ActualEffort { get; set; }

        public static List<CompletedTaskItem> GetItems()
        {

            string[] nameDummyTexts = new[]
            {
                @"Work on Design project",
                @"Study for Science",
                @"Study for History",
                @"Write English essay"
            };

            string[] descriptionDummyTexts = new[]
            {
                @"sjflaesf s",
                @"slfjlsafd sa",
                @"sjfl ass",
                @"slfjselafd",
            };

            DateTime dummyDueDate = new DateTime();
            dummyDueDate = DateTime.Now;
            
            Random random = new Random();
            int dummyEstimatedEffort = random.Next(0, 50);  

            List<CompletedTaskItem> objects = new List<CompletedTaskItem>();
            objects.Add(new CompletedTaskItem()
            {
                Name = nameDummyTexts[random.Next(0, 3)],
                Description = descriptionDummyTexts[random.Next(0, 3)],
                DueDate = dummyDueDate,
                ExpectedEffort = dummyEstimatedEffort,
            });
            return objects;
        }
    }
}
