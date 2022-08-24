using AnyDo.Models;
using Domain;

namespace AnyDo.Mappers
{
    public static class TaskMapper
    {
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
