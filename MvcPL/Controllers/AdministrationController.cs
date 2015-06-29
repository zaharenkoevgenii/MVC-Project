using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;
using MvcPL.Utils;

namespace MvcPL.Controllers
{
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

        public ActionResult Index()
        {
            return View(_uservice
                .Get()
                .OrderBy(user => user.Email)
                .Select(user => new UserViewModel
                {
                    Email = user.Email,
                    Id = user.Id
                }));
        }

        //public ActionResult SearchUEntry()
        //{
        //    string tag = Request.Form.Get("tagInput");
        //    var entry=_uservice
        //        .Get()
        //        .Select(user => new UserViewModel
        //        {
        //              Email = user.Email,
        //              Id = user.Id
        //            });
        //    if (!string.IsNullOrEmpty(tag))
        //    {
        //        entry = entry.Where(s => s.Email.Contains(tag));
        //    }
        //    if (!entry.Any())
        //    {
        //        return new HtmlResult("Пользователь отсутствует","entries");
        //    }
        //    return PartialView("_UserPartial",entry);
        //}

        //public ActionResult SearchFEntry()
        //{
        //    var tag = Request.Form.Get("tagInput");
        //    var entry = _fservice
        //        .Get()
        //        .Select(file => new ExtendedFileViewModel
        //        {
        //            Name = file.Name,
        //            Id = file.Id,
        //            OwnerId = file.UserId,
        //            Created = file.CreationTime,
        //            OwnerName = _uservice.Get().First(user => user.Id == file.UserId).Email
        //        });
        //    if (!string.IsNullOrEmpty(tag))
        //    {
        //        entry = entry.Where(s => s.Name.Contains(tag));
        //    }
        //    if (!entry.Any())
        //    {
        //        return new HtmlResult("Файл отсутствует", "entries");
        //    }
        //    return PartialView("_FilePartial", entry);
        //}

        public ActionResult Approve()
        {
            return View(_fservice.Get().Where(f => f.Approved == false));
        }

        public ActionResult ApproveFile(int id)
        {
            var file = _fservice.Get().First(f => f.Id == id);
            file.Approved = true;
            _fservice.Add(file);
            return RedirectToAction("Approve");
        }
        public ActionResult FileManage(int id)
        {
            var data = _fservice
                .Get()
                .OrderBy(file => file.Name)
                .Select(file => new ExtendedFileViewModel
                {
                    Name = file.Name,
                    Id = file.Id,
                    OwnerId = file.UserId,
                    OwnerName = _uservice.Get().First(user => user.Id == file.UserId).Email,
                    Created = file.CreationTime
                });
                data = data.Where(file => file.OwnerId == id);
            return View(data);
        }

    }
}
