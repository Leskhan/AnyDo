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

        public async void AddTask(TaskEntity task)
        {
            string sql = $@"INSERT INTO tasks (name, notes, created_date, end_date, list_id, is_completed) 
                            VALUES (@{nameof(task.Name)}, 
                                    @{nameof(task.Notes)},
                                    @{nameof(task.CreatedDate)},
                                    @{nameof(task.EndDate)},
                                    @{nameof(task.ListEntityId)},
                                    @{nameof(task.IsCompleted)})";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql, task);
            }
        }

        public async void DeleteTaskById(int id)
        {
            string sql = $@"DELETE FROM tasks
                            WHERE id = {id}";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql);
            }
        }

        public async Task<List<TaskEntity>> GetAllTasks()
        {
            var list = new List<TaskEntity>();
            string sql = @"SELECT tasks.id AS Id,
	                                tasks.name AS Name, 
	                                tasks.notes AS Notes,
	                                tasks.created_date AS CreatedDate,
	                                tasks.end_date AS EndDate,
                                    lists.id AS ListEntityId,
                                    tasks.is_completed AS IsCompleted,
	                                lists.id AS Id,
                                    lists.name AS Name
                                FROM tasks
	                                JOIN lists ON tasks.list_id = lists.id;";
            
            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                list = (List<TaskEntity>)await db.QueryAsync<TaskEntity, ListEntity, TaskEntity>(sql, (task, list) => { task.List = list; return task; }, splitOn: "Id");
            }

            return list;
        }

        public async void UpdateTask(TaskEntity task)
        {
            string sql = $@"UPDATE tasks 
                            SET name = @{nameof(task.Name)},
                                notes = @{nameof(task.Notes)},
                                created_date = @{nameof(task.CreatedDate)},
                                end_date = @{nameof(task.EndDate)},
                                list_id = @{nameof(task.ListEntityId)},
                                is_completed = @{nameof(task.IsCompleted)}
                            WHERE id = @{nameof(task.Id)};";

            using (IDbConnection db = new SqliteConnection(_stringConnection))
            {
                await db.ExecuteAsync(sql, task);
            }
        }
    }
}
