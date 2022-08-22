﻿using Data;
using Entities;

namespace DBTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathDirectory = Environment.CurrentDirectory;
            var locationProject = pathDirectory.Substring(0, pathDirectory.IndexOf("AnyDo"));
            string file = @"AnyDo\Data\AnyDoDB.db";
            string fileCopy = @"C:\Users\user\source\repos\AnyDo\Data\AnyDoDB-copy.db";

            string locationDb = locationProject + file;

            //File.Copy(@"C:\Users\user\source\repos\AnyDo\Data\AnyDoDB.db", @"C:\Users\user\source\repos\AnyDo\Data\AnyDoDB-copy.db");
            //File.SetAttributes(@"C:\Users\user\source\repos\AnyDo\Data\AnyDoDB-copy.db", FileAttributes.Normal);

            var taskRepository = new TaskRepository("Data Source=" + locationDb);

            TaskEntity newTask = new TaskEntity()
            {
                Name = "Test Name",
                Notes = "Test Notes",
                CreatedDate = "22.08.2022",
                EndDate = "24.08.2022",
                ListEntityId = 1
            };

            taskRepository.AddTask(newTask);
            newTask.Id = 3;
            newTask.Name = "Leskhan";
            taskRepository.UpdateTask(newTask);

            taskRepository.DeleteTaskById(3);

            var tasks = taskRepository.GetAllTasks().Result;
        }
    }
}