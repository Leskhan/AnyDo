using AnyDo.Mappers;
using AnyDo.Models;
using AnyDo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AnyDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : Controller
    {
        private IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        /// <summary>
        /// Gets all lists from databases
        /// </summary>
        /// <returns>List of lists</returns>
        [HttpGet]
        public List<ListModel> GetLists()
        {
            return _listService.GetLists().ToModelList();
        }

        /// <summary>
        /// Adds a new list to the database
        /// </summary>
        /// <param name="listVM"></param>
        /// <remarks>
        /// Sample:
        /// 
        ///     {
        ///        "name": "Univer"
        ///     }
        /// </remarks>
        /// <response code="400">List name must not be empty</response>
        [HttpPost]
        public IActionResult AddList(ListViewModel listVM)
        {
            if (listVM == null || listVM.Name == null || listVM.Name == "string" || listVM.Name == "")
                return new BadRequestResult();

            ListModel model = new ListModel() { Name = listVM.Name };

            _listService.AddList(model.ToDomain());
            return new OkResult();
        }

        /// <summary>
        /// Updates the name of an existing list
        /// </summary>
        /// <param name="listVM"></param>
        /// <remarks>
        /// You must specify the id of the updated list and the new name.
        /// Sample:
        /// 
        /// Existing list
        /// 
        ///     {
        ///        "id": 2,
        ///        "name": "Business"
        ///     }
        ///     
        /// Let's give a new name    
        /// 
        ///     {
        ///        "id": 2,
        ///        "name": "Park"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateList(ListViewModelUpdate listVM)
        {
            if (listVM == null || listVM.Name == null || listVM.Name == "string" || listVM.Name == "")
                return new BadRequestResult();

            ListModel model = new ListModel() 
            {
                Id = listVM.Id,
                Name = listVM.Name
            };

            _listService.UpdateList(model.ToDomain());
            return new OkResult();
        }

        /// <summary>
        /// Removes a list from databases by id
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpDelete("{listId}")]
        public IActionResult DeleteListById(int listId)
        {
            if (listId == 0)
                return new BadRequestResult();

            _listService.DeleteListById(listId);
            return new OkResult();
        }
    }
}
