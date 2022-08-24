using AnyDo.Mappers;
using AnyDo.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AnyDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : Controller
    {
        private IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        private ListModel list = new ListModel() { Id = 1, Name = "Personal" };

        [HttpGet]
        public List<ListModel> GetLists()
        {
            return _listService.GetLists().ToModelList();
        }
    }
}
