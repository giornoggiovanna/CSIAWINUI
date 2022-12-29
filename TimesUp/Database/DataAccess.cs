using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Windows.Storage;

namespace TimesUp.Database
{
    public static class DataAccess
    {
        private const string DatabaseName = "TimesUp.db";

        public async static void InitializeDatabase()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            await ApplicationData.Current.LocalFolder.CreateFileAsync(DatabaseName, CreationCollisionOption.OpenIfExists);
            string dbPath = GetDatabasePath();

            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                var tasksTable = """
                    create table if not exists Tasks 
                    (
                        TaskId char(16) primary key not null,
                        TaskName nvarchar(50) not null,
                        TaskDescription nvarchar(500) not null,
                        TaskDueDate date not null,
                        TaskExpectedEffort int not null,
                        TaskCurrentEffort int not null
                    )
                """;

                var command = new SqliteCommand(tasksTable, db);

                command.ExecuteNonQuery();
            }
        }

        private static string GetDatabasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, DatabaseName);
        }

        public static void AddTask(Task task) 
        {
            string dbPath = GetDatabasePath();
            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                var insertCommand = new SqliteCommand();
                insertCommand.Connection= db;

                insertCommand.CommandText = "insert into Tasks (TaskId, TaskName, TaskDescription, TaskDueDate, TaskExpectedEffort, TaskCurrentEffort) values (@taskId, @taskName, @taskDescription, @taskDueDate, @taskExpectedEffort, @taskCurrentEffort);";
                insertCommand.Parameters.AddWithValue("@taskId", task.Id);
                insertCommand.Parameters.AddWithValue("@taskName", task.Name);
                insertCommand.Parameters.AddWithValue("@taskDescription", task.Description);
                insertCommand.Parameters.AddWithValue("@taskDueDate", task.DueDate);
                insertCommand.Parameters.AddWithValue("@taskExpectedEffort", task.ExpectedEffort);
                insertCommand.Parameters.AddWithValue("@taskCurrentEffort", task.CurrentEffort);
            
                insertCommand.ExecuteReader();
            }  
        }

    }
}
