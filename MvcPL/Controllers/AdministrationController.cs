using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using PagedList;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "user")]
    [HandleAllError]
    public class AdministrationController : Controller
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<FileEntity> _fservice;

        public AdministrationController(IService<UserEntity> uservice, IService<FileEntity> fservice)
        {
            _fservice = fservice;
            _uservice = uservice;
        }

        public ActionResult Approve(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord = _fservice.Get().Where(f => f.Approved == false);
            if (Request.IsAjaxRequest())
                return PartialView("ApproveFile",listRecord.ToPagedList(pageNumber, pageSize));
            return View("ApproveFile", listRecord.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ApproveFile(int id)
        {
            var file = _fservice.Get().First(f => f.Id == id);
            file.Approved = true;
            _fservice.Add(file);
            return RedirectToAction("Approve");
        }

        public ActionResult UserManage(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord = _uservice.Get().Where(u => !u.Roles.Select(r => r.Name).Contains("admin"));
            if (Request.IsAjaxRequest())
                return PartialView("UserManage", listRecord.ToPagedList(pageNumber, pageSize));
            return View("UserManage", listRecord.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FileManage(int id,int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 6;
            var listRecord = _fservice.Get().Where(f => f.UserId == id);
            if (Request.IsAjaxRequest())
                return PartialView("ApproveFile", listRecord.ToPagedList(pageNumber, pageSize));
            return View("ApproveFile", listRecord.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MyFilesManage(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 10;
            var listRecord = _fservice.Get().Where(f => f.UserId == _uservice.Search(u=>u.Email==User.Identity.Name).First().Id);
            return PartialView("_SimpleFileManage", listRecord.ToPagedList(pageNumber, pageSize));
        }
    }
}
