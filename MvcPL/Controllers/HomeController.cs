using System.Collections.Generic;
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
            return View(_service.Get().Where(f => !f.Private).OrderByDescending(f => f.Rating));
        }
        public ActionResult MusicView()
        {
            return View(_service.Get().Where(f => !f.Private).Where(f => f.ContentType.Contains("audio")).OrderByDescending(f => f.Rating));
        }
        public ActionResult DocumentsView()
        {
            return View(_service.Get().Where(f => !f.Private).Where(f => f.ContentType.Contains("multipart") || f.ContentType.Contains("text") || f.ContentType.Contains("application")).OrderByDescending(f => f.Rating));
        }
        public ActionResult ImageView()
        {
            return View(_service.Get().Where(f => !f.Private).Where(f => f.ContentType.Contains("image")).OrderByDescending(f => f.Rating));
        }
    }
}