using AnyDo.Models;
using Domain;

namespace AnyDo.Mappers
{
    public static class ListMapper
    {
        public static ListModel ToModel(this ListDomain list)
        {
            if (list is null)
                return null;

            return new ListModel()
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.Tasks.ToModelList()
            };
        }

        public static ListDomain ToDomain(this ListModel list)
        {
            return new ListDomain() 
            {
                Id = list.Id,
                Name = list.Name
            };
        }

        public static List<ListModel> ToModelList(this List<ListDomain> lists)
        {
            if (lists is null)
                return null;

            return lists.Select(t => t.ToModel()).ToList();
        }
    }
}
