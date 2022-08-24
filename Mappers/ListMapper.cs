using AnyDo.Models;
using Domain;
using Entities;

namespace Mappers
{
    public static class ListMapper
    {
        public static ListEntity ToEntity(this ListDomain list)
        {
            return new ListEntity() 
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public static ListDomain ToDomain(this ListEntity list)
        {
            return new ListDomain() 
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public static ListModel ToModel(this ListDomain list)
        {
            return new ListModel()
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.Tasks.ToModelList()
            };
        }

        public static List<TaskDomain> ToDomainList(this List<TaskEntity> tasks)
        {
            return tasks.Select(t => t.ToDomain()).ToList();
        }

        public static List<TaskModel> ToModelList(this List<TaskDomain> tasks)
        {
            return tasks.Select(t => t.ToModel()).ToList();
        }
    }
}
