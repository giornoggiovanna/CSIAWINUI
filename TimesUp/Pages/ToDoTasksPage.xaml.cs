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
using Windows.UI.StartScreen;

namespace TimesUp.Pages
{
    public sealed partial class ToDoTasksPage : Page
    {
        ObservableCollection<TaskItem> tasks = new();

        public ToDoTasksPage()
        {
            this.InitializeComponent();
            
        }

        private void ToDoListGridview_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTask();   
        }

        private void CreateNewTask()
        {

            Random random = new();
            var tempList = TaskItem.GetItems();
            ObservableCollection<TaskItem> tempTaskItem = new(tempList);
            tasks.Add(new TaskItem() { Name = "slakfsef", Description = @"sfjlesajfe", DueDate= DateTime.Now, ExpectedEffort = random.Next(0, 50), CurrentEffort = random.Next(0, 50), RemainingEffort = random.Next(0, 49)});    

            ToDoListGridview.ItemsSource = tasks;
        }
    }


    public class TaskItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ExpectedEffort { get; set; }
        public int CurrentEffort { get; set; }
        public int RemainingEffort { get; set; }

        public static List<TaskItem> GetItems()
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

            DateTime dummyDueDate = new();
            dummyDueDate = DateTime.Now;

            Random random = new();
            int dummyEstimatedEffort = random.Next(0, 50);

            List<TaskItem> objects = new();
            objects.Add(new TaskItem()
            {
                Name = nameDummyTexts[random.Next(0, 3)],
                Description = descriptionDummyTexts[random.Next(0, 3)],
                DueDate = dummyDueDate,
                ExpectedEffort = dummyEstimatedEffort,
                CurrentEffort = random.Next(0, 50),
                RemainingEffort = random.Next(0, 50)
            });
            return objects;
        }

    }
}
