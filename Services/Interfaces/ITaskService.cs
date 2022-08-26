using Domain;

namespace Services.Interfaces
{
    public interface ITaskService
    {
        List<TaskDomain> GetAllTasks();
        void AddTask(TaskDomain task);
        void UpdateTask(TaskDomain task);
        void DeleteTaskById(int taskId);
        void UpdateTaskStatus(int taskId, bool isCompleted);
    }
}
