using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using PagedList;

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
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord = _service
                .Search(f=>!f.Private)
                .Where(f => f.Approved)
                .OrderByDescending(f => f.Rating);
            if (Request.IsAjaxRequest())
                return PartialView(listRecord.ToPagedList(pageNumber, pageSize));
            return View(listRecord.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult MusicView(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord =
                _service.Get()
                    .Where(f => !f.Private)
                    .Where(f=>f.Approved)
                    .Where(f => f.ContentType.Contains("audio"))
                    .OrderByDescending(f => f.Rating);
            if (Request.IsAjaxRequest())
                return PartialView(listRecord.ToPagedList(pageNumber, pageSize));
            return View(listRecord.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DocumentsView(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord =
                _service.Get()
                    .Where(f => !f.Private)
                    .Where(f => f.Approved)
                    .Where(f => f.ContentType.Contains("application"))
                    .OrderByDescending(f => f.Rating);
            if (Request.IsAjaxRequest())
                return PartialView(listRecord.ToPagedList(pageNumber, pageSize));
            return View(listRecord.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ImageView(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord =
                _service.Get()
                    .Where(f => !f.Private)
                    .Where(f => f.Approved)
                    .Where(f => f.ContentType.Contains("image"))
                    .OrderByDescending(f => f.Rating);
            if (Request.IsAjaxRequest())
                return PartialView(listRecord.ToPagedList(pageNumber, pageSize));
            return View(listRecord.ToPagedList(pageNumber, pageSize));
        }
    }
}