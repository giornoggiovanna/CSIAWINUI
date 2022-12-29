using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
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
            var dbPath = GetDatabasePath();

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
            var dbPath = GetDatabasePath();

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
            
                insertCommand.ExecuteNonQuery();
            }  
        }

        public static List<Task> GetToDoTasks()
        {
            var dbPath = GetDatabasePath();

            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                var selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "select TaskId, TaskName, TaskDescription, TaskDueDate, TaskExpectedEffort, TaskCurrentEffort from Tasks;";

                var query = selectCommand.ExecuteReader();

                var tasks = new List<Task>();

                while (query.Read())
                {
                    var task = new Task
                    {
                        Id = query.GetGuid(0),
                        Name = query.GetString(1),
                        Description = query.GetString(2),
                        DueDate = DateOnly.FromDateTime(query.GetDateTime(3)),
                        ExpectedEffort = query.GetInt32(4),
                        CurrentEffort = query.GetInt32(5)
                    };

                    tasks.Add(task);
                }

                return tasks;
            }
        }

        public static Task GetTask(Guid taskId)
        {
            var dbPath = GetDatabasePath();

            using (var db = new SqliteConnection($"Filename={dbPath}"))
            {
                db.Open();

                var selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "select TaskId, TaskName, TaskDescription, TaskDueDate, TaskExpectedEffort, TaskCurrentEffort from Tasks where TaskId = @taskId;";
                selectCommand.Parameters.AddWithValue("@taskId", taskId);

                var query = selectCommand.ExecuteReader();

                if(query.Read())
                {
                    var task = new Task
                    {
                        Id = query.GetGuid(0),
                        Name = query.GetString(1),
                        Description = query.GetString(2),
                        DueDate = DateOnly.FromDateTime(query.GetDateTime(3)),
                        ExpectedEffort = query.GetInt32(4),
                        CurrentEffort = query.GetInt32(5)
                    };

                    return task;
                }
                else
                {
                    throw new Exception("Can't find task!!");
                }
            }
        }
    }
}
