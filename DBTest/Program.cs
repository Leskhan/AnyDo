using Data;
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
                ListEntityId = 1,
                IsCompleted = "false"
            };

            newTask.Id = 2;
            newTask.Name = "Leskhan";
            //taskRepository.AddTask(newTask);
            //taskRepository.UpdateTask(newTask);
            //taskRepository.DeleteTaskById(3);
            //var tasks = taskRepository.GetAllTasks().Result;


            ListRepository listRepository = new ListRepository("Data Source=" + locationDb);

            ListEntity newList = new ListEntity()
            {
                Id = 3,
                Name = "Home"
            };

            //listRepository.DeleteListById(newList.Id);
            //listRepository.UpdateList(newList);
            //listRepository.AddList(newList);
        }
    }
}