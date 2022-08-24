using Entities;

namespace Data.Repositories
{
    public interface ITaskRepository
    {
        List<TaskEntity> GetAllTasks();
        void AddTask(TaskEntity task);
        void UpdateTask(TaskEntity task);
        void DeleteTaskById(int id);
    }
}
