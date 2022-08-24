using Domain;

namespace Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDomain>> GetAllTasks();
        void AddTask(TaskDomain task);
        void UpdateTask(TaskDomain task);
        void DeleteTaskById(int taskId);
    }
}
