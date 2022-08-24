using Domain;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TaskService : ITaskService
    {
        public void AddTask(TaskDomain task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTaskById(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskDomain>> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(TaskDomain task)
        {
            throw new NotImplementedException();
        }
    }
}
