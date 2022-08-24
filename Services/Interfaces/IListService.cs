using Domain;

namespace Services.Interfaces
{
    public interface IListService
    {
        ListDomain GetListById(int listId);
        void AddList(ListDomain list);
        void UpdateList(ListDomain list);
        void DeleteListById(int listId);
        List<ListDomain> GetLists();
    }
}
