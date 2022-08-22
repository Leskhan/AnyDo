using Data.Repositories;
using Entities;

namespace Data
{
    public class ListRepository : IListRepository
    {
        private string _stringConnection;

        public ListRepository(string stringConnection)
        {
            _stringConnection = stringConnection;
        }

        public void AddList(ListEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteListById(int id)
        {
            throw new NotImplementedException();
        }

        public ListEntity GetList(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateList(ListEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
