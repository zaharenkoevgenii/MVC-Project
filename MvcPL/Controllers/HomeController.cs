using System.Linq;
using System.Web.Mvc;
using System.Web.Providers;
using System.Web.Providers.Entities;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        public HomeController(IUserService service)
        {
            this._service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetN(5)
                .Select(user => new UserViewModel()
                {
                    UserName = user.UserName,
                    UserId = user.Id.ToString()
                }));
        }
    }
}