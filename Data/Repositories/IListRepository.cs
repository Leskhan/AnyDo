using Entities;

namespace Data.Repositories
{
    public interface IListRepository
    {
        ListEntity GetListById(int id);
        void AddList(ListEntity list);
        void UpdateList(ListEntity list);
        void DeleteListById(int id);
        List<ListEntity> GetLists();
        List<TaskEntity> GetListTasks(int listId);
    }
}
