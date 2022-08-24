using Data.Repositories;
using Domain;
using Mappers;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ListService : IListService
    {
        private IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public void AddList(ListDomain list)
        {
            _listRepository.AddList(list.ToEntity());
        }

        public void DeleteListById(int listId)
        {
            _listRepository.DeleteListById(listId);
        }

        public ListDomain GetListById(int listId)
        {
            var listDomain = _listRepository.GetListById(listId).ToDomain();
            listDomain.Tasks = _listRepository.GetListTasks(listId).ToDomainList();
            return listDomain;
        }

        public List<ListDomain> GetLists()
        {
            return _listRepository.GetLists().ToDomainList();
        }

        public void UpdateList(ListDomain list)
        {
            _listRepository.UpdateList(list.ToEntity());
        }
    }
}
