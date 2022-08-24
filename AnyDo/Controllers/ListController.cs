using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AnyDo.Controllers
{
    public class ListController : Controller
    {
        private IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }
    }
}
