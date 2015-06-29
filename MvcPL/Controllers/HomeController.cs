using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [HandleAllError]
    public class HomeController : Controller
    {
        private readonly IService<FileEntity> _service;
        public HomeController(IService<FileEntity> service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.Get().OrderBy(f=>f.Rating));
        }
    }
}