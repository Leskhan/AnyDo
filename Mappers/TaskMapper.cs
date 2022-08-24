using AnyDo.Models;
using Domain;
using Entities;


namespace Mappers
{
    public static class TaskMapper
    {
        public static TaskDomain ToDomain(this TaskEntity task)
        {
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

        public static TaskModel ToModel(this TaskDomain task)
        {
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
    }
}
