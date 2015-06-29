using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [HandleAllError]
    public class HomeController : Controller
    {
        private readonly IService<UserEntity> _service;
        public HomeController(IService<UserEntity> service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.Get().Select(user=>new UserViewModel()
            {
                Email = user.Email,
                Id=user.Id,
                Roles = user.Roles,
                Files = user.Files,
                Profile = user.Profile
            }));
        }
    }
}