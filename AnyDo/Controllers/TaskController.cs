﻿using AnyDo.Mappers;
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
        /// Returns all tasks from databases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TaskModel> GetAllTasks() 
        {
            return _tasks;
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
            existingTask.IsCompleted = taskVM.IsCompleted;
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
        /// <param name="taskId"></param>
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


    }
}
