using Dapper;
using Data.Repositories;
using Entities;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Data
{
    public class ListRepository : IListRepository
    {
        private string _stringConnection;

        public ListRepository(string stringConnection)
        {
            _stringConnection = stringConnection;
        }

        public async void AddList(ListEntity list)
        {
            string sql = $@"INSERT INTO lists (name)
                            VALUES (@{nameof(list.Name)})";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql, list);
            }
        }

        public async void DeleteListById(int id)
        {
            string sql = @$"DELETE FROM lists
                            WHERE id = {id}";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql);
            }
        }

        public ListEntity GetListById(int id)
        {
            ListEntity list;
            string sql = $@"SELECT id AS Id,
                                   name AS Name
                            FROM lists
                            WHERE id = {id}";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                list = db.Query<ListEntity>(sql).First();
            }

            return list;
        }

        public List<ListEntity> GetLists()
        {
            List<ListEntity> list;
            string sql = @"SELECT id AS Id,
                                    name AS Name
                           FROM lists";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                list = db.Query<ListEntity>(sql).ToList();
            }

            return list;
        }

        public List<TaskEntity> GetListTasks(int listId)
        {
            List<TaskEntity> list;
            string sql = $@"SELECT tasks.id AS Id,
	                                tasks.name AS Name, 
	                                tasks.notes AS Notes,
	                                tasks.created_date AS CreatedDate,
	                                tasks.end_date AS EndDate,
                                    lists.id AS ListEntityId,
                                    tasks.is_completed AS IsCompleted,
	                                lists.id AS Id,
                                    lists.name AS Name
                                FROM tasks
	                                JOIN lists ON tasks.list_id = lists.id
								WHERE list_id = {listId};";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                list = (List<TaskEntity>)db.Query<TaskEntity, ListEntity, TaskEntity>(sql, (task, list) => { task.List = list; return task; }, splitOn: "Id");
            }

            return list;
        }

        public async void UpdateList(ListEntity list)
        {
            string sql = $@"UPDATE lists
                            SET name = @{nameof(list.Name)}
                            WHERE id = @{nameof(list.Id)}";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql, list);
            }
        }
    }
}
