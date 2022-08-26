using Data.Repositories;
using Domain;
using Mappers;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTask(TaskDomain task)
        {
            _taskRepository.AddTask(task.ToEntity());
        }

        public void DeleteTaskById(int taskId)
        {
            _taskRepository.DeleteTaskById(taskId);
        }

        public List<TaskDomain> GetAllTasks()
        {
            return _taskRepository.GetAllTasks().ToDomainList();
        }

        public void UpdateTask(TaskDomain task)
        {
            _taskRepository.UpdateTask(task.ToEntity());
        }

        public void UpdateTaskStatus(int taskId, bool isCompleted)
        {
            _taskRepository.UpdateTaskStatus(taskId, isCompleted);
        }
    }
}
