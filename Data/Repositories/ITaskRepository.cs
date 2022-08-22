using Entities;

namespace Data.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskEntity>> GetAllTasks();
        void AddTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void DeleteTaskById(int id);
    }
}
