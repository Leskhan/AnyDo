using AnyDo.Models;
using Domain;

namespace AnyDo.Mappers
{
    public static class ListMapper
    {
        public static ListModel ToModel(this ListDomain list)
        {
            return new ListModel()
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.Tasks.ToModelList()
            };
        }

        public static List<TaskModel> ToModelList(this List<TaskDomain> tasks)
        {
            return tasks.Select(t => t.ToModel()).ToList();
        }
    }
}
