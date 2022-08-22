using Dapper;
using Data.Repositories;
using Entities;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Data
{
    public class TaskRepository : ITaskRepository
    {
        private string _stringConnection;

        public TaskRepository(string stringConnection)
        {
            _stringConnection = stringConnection;
        }

        public void AddTask(TaskEntity task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(TaskEntity task)
        {
            throw new NotImplementedException();
        }

        public List<TaskEntity> GetAllTasks()
        {
            var list = new List<TaskEntity>();
            string sql = @"SELECT tasks.id AS Id,
	                                tasks.name AS Name, 
	                                tasks.notes AS Notes,
	                                tasks.created_date AS CreatedDate,
	                                tasks.end_date AS EndDate,
                                    lists.id AS ListEntityId,
	                                lists.id AS Id,
                                    lists.name AS Name
                                FROM tasks
	                                JOIN lists ON tasks.list_id = lists.id;";
            
            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                list = db.Query<TaskEntity, ListEntity, TaskEntity>(sql, (task, list) => { task.List = list; return task; }, splitOn: "Id").ToList();
            }

            return list;
        }

        public void UpdateTask(TaskEntity task)
        {
            throw new NotImplementedException();
        }
    }
}
