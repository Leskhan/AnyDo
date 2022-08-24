//using AnyDo.Models;
using Domain;
using Entities;

namespace Mappers
{
    public static class ListMapper
    {
        public static ListEntity ToEntity(this ListDomain list)
        {
            if (list is null)
                return null;

            return new ListEntity() 
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public static ListDomain ToDomain(this ListEntity list)
        {
            if (list is null)
                return null;

            return new ListDomain() 
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public static List<ListDomain> ToDomainList(this List<ListEntity> lists)
        {
            if (lists is null)
                return null;

            return lists.Select(t => t.ToDomain()).ToList();
        }

    }
}
