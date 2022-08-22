using Data;

namespace DBTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathDirectory = Environment.CurrentDirectory;
            var locationProject = pathDirectory.Substring(0, pathDirectory.IndexOf("AnyDo"));
            string locationDb = @"AnyDo\Data\AnyDoDB.db";
            string stringConnection = locationProject + locationDb;

            var taskRepository = new TaskRepository("Data Source=" + stringConnection);

            var tasks = taskRepository.GetAllTasks();
        }
    }
}