//using AnyDo.Models;
using Domain;
using Entities;

namespace Mappers
{
    public static class TaskMapper
    {
        public static TaskDomain ToDomain(this TaskEntity task)
        {
            if (task is null)
                return null;

            return new TaskDomain() 
            {
                Id = task.Id,
                Name = task.Name,
                EndDate = task.EndDate.ToDate(),
                CreatedDate = task.CreatedDate.ToDate(),
                Notes = task.Notes,
                IsCompleted = task.IsCompleted.ToBool(),
                ListDomainId = task.ListEntityId,
                List = task.List.ToDomain()
            };
        }

        public static TaskEntity ToEntity(this TaskDomain task)
        {
            if (task is null)
                return null;

            return new TaskEntity()
            {
                Id = task.Id,
                Name = task.Name,
                EndDate = task.EndDate.ToString(),
                CreatedDate = task.CreatedDate.ToString(),
                Notes = task.Notes,
                IsCompleted = task.IsCompleted.ToString(),
                ListEntityId = task.ListDomainId,
                List = task.List.ToEntity()
            };
        }

        public static List<TaskDomain> ToDomainList(this List<TaskEntity> tasks)
        {
            if (tasks is null)
                return null;

            return tasks.Select(t => t.ToDomain()).ToList();
        }
    }
}
