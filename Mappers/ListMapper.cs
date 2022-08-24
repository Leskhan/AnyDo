//using AnyDo.Models;
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

        public static List<ListDomain> ToDomainList(this List<ListEntity> tasks)
        {
            return tasks.Select(t => t.ToDomain()).ToList();
        }

    }
}
