using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Services;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        public HomeController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetN(5).Select(user=>user.UserName));
        }
    }
}