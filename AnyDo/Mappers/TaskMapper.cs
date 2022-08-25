using AnyDo.Models;
using Domain;

namespace AnyDo.Mappers
{
    public static class TaskMapper
    {
        public static TaskModel ToModel(this TaskDomain task)
        {
            if (task is null)
                return null;

            return new TaskModel()
            {
                Id = task.Id,
                Name = task.Name,
                EndDate = task.EndDate,
                CreatedDate = task.CreatedDate,
                Notes = task.Notes,
                IsCompleted = task.IsCompleted,
                ListModelId = task.ListDomainId,
                List = task.List.ToModel()
            };
        }

        public static TaskDomain ToDomain(this TaskModel task)
        {
            if (task is null)
                return null;

            return new TaskDomain()
            {
                Id = task.Id,
                Name = task.Name,
                EndDate = task.EndDate,
                CreatedDate = task.CreatedDate,
                Notes = task.Notes,
                IsCompleted = task.IsCompleted,
                ListDomainId = task.ListModelId,
                List = null
            };
        }

        public static List<TaskModel> ToModelList(this List<TaskDomain> tasks)
        {
            if (tasks is null)
                return null;

            return tasks.Select(t => t.ToModel()).ToList();
        }
    }
}
