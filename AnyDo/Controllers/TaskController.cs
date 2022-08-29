using AnyDo.Mappers;
using AnyDo.Models;
using AnyDo.ViewModels;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AnyDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController
    {
        private ITaskService _taskService;
        private List<TaskModel> _tasks;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
            _tasks = _taskService.GetAllTasks().ToModelList();
        }

        /// <summary>
        /// Returns all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllTasks")]
        public List<TaskModel> AllTasks()
        {
            return _tasks;
        }

        /// <summary>
        /// Returns tasks whose due date is today
        /// </summary>
        /// <returns></returns>
        [HttpGet("Today")]
        public List<TaskModel> Today()
        {
            return _tasks.Where(task => task.EndDate == null || task.EndDate == DateTime.Today).ToList();
        }

        /// <summary>
        /// Returns tasks whose due date is less than 7 days
        /// </summary>
        /// <returns></returns>
        [HttpGet("Next7Days")]
        public List<TaskModel> Next7Days()
        {
            return _tasks.Where(task => task.EndDate < DateTime.Today.AddDays(7) || task.EndDate == null).ToList();
        }


        [HttpGet("ListTasks/{listName}")]
        public List<TaskModel> ListTasks(string listName)
        {
            return _tasks.Where(task => task.List.Name == listName).ToList();
        }

        /// <summary>
        /// Updates the task
        /// </summary>
        /// <param name="taskVM"></param>
        /// <returns></returns>
        /// <remarks>
        /// Updates the task with the specified id
        /// 
        /// Existing task
        /// 
        ///     {
        ///        "id": 2,
        ///        "name": "Leskhan",
        ///        "notes": "Test Notes",
        ///        "endDate": "2022-08-24T00:00:00",
        ///        "isCompleted": false,
        ///        "listModelId": 1
        ///     }
        ///     
        /// After changing properties
        /// 
        ///     {
        ///        "id": 2,
        ///        "name": "Learn Docker",
        ///        "notes": "What is Docker?",
        ///        "endDate": "2022-08-24T00:00:00",
        ///        "isCompleted": true,
        ///        "listModelId": 2
        ///     }
        /// 
        /// </remarks>
        [HttpPut]
        public IActionResult UpdateTask(TaskViewModelUpdate taskVM)
        {
            if (taskVM == null || taskVM.Name == "string" || taskVM.Name == "")
                return new BadRequestResult();

            var existingTask = _tasks.FirstOrDefault(x => x.Id == taskVM.Id);

            if (existingTask is null)
                return new BadRequestResult();

            existingTask.Id = taskVM.Id;
            existingTask.Name = taskVM.Name;
            existingTask.Notes = taskVM.Notes;
            existingTask.EndDate = taskVM.EndDate;
            existingTask.CreatedDate = existingTask.CreatedDate;
            existingTask.IsCompleted = existingTask.IsCompleted;
            existingTask.ListModelId = taskVM.ListModelId;

            _taskService.UpdateTask(existingTask.ToDomain());
            return new OkResult();
        }

        /// <summary>
        /// Adds a new task to the database
        /// </summary>
        /// <param name="taskViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTask([FromBody] TaskViewModel taskViewModel)
        {
            if (taskViewModel.Name == "string" || taskViewModel.Name == "")
                return new BadRequestResult();

            TaskModel task = new TaskModel() 
            {
                Name = taskViewModel.Name,
                Notes = taskViewModel.Notes,
                EndDate = taskViewModel.EndDate.ToDate(), 
                CreatedDate = DateTime.Now,
                IsCompleted = false,
                ListModelId = taskViewModel.ListModelId == 0 ? 1 : taskViewModel.ListModelId
            };

            _taskService.AddTask(task.ToDomain());
            _tasks.Add(task);

            return new OkResult();
        }

        /// <summary>
        /// Deletes Deletes a task by id
        /// </summary>
        /// <param name="taskId">Task id</param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        public IActionResult DeleteTaskById(int taskId)
        {
            if (taskId == 0)
                return new BadRequestResult();

            var existingTask = _tasks.FirstOrDefault(t => t.Id == taskId);

            if (existingTask is null)
                return new BadRequestResult();

            _tasks.Remove(existingTask);
            _taskService.DeleteTaskById(taskId);
            return new OkResult();
        }

        /// <summary>
        /// Changes the status of a task
        /// </summary>
        /// <param name="taskId">The ID of the task we want to change</param>
        /// <param name="isCompleted">New value</param>
        /// <returns></returns>
        [HttpPut("[action]/{taskId}/{isCompleted}")]
        public IActionResult UpdateTaskStatus(int taskId, bool isCompleted)
        {
            var existingTask = _tasks.FirstOrDefault(task => task.Id == taskId);
            if (existingTask is not null)
            {
                existingTask.IsCompleted = isCompleted;
                _taskService.UpdateTaskStatus(taskId, isCompleted);
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
