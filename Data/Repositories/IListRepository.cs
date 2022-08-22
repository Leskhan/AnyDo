using Entities;

namespace Data.Repositories
{
    public interface IListRepository
    {
        ListEntity GetList(int id);
        void AddList(ListEntity entity);
        void UpdateList(ListEntity entity);
        void DeleteList(int id);
    }
}
