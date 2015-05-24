using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using BLL.Interface.Services;
using MvcPL.Models;
using MvcPL.Utils;

namespace MvcPL.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IFileService _fservice;
        private readonly IUserService _uservice;
        public AdministrationController(IFileService fservice, IUserService uservice)
        {
            this._fservice = fservice;
            this._uservice = uservice;
        }

        public ActionResult Index()
        {
            return View(_uservice
                .GetAllUserEntities()
                .OrderBy(user => user.UserName)
                .Select(user => new UserViewModel()
                {
                    UserName = user.UserName,
                    UserId = user.Id.ToString()
                }));
        }

        public ActionResult SearchUEntry()
        {
            string tag = Request.Form.Get("tagInput");
            var entry=_uservice
                .GetAllUserEntities()
                .Select(user => new UserViewModel()
                    {
                      UserName = user.UserName,
                      UserId = user.Id.ToString()
                    });
            if (!string.IsNullOrEmpty(tag))
            {
                entry = entry.Where(s => s.UserName.Contains(tag));
            }
            if (!entry.Any())
            {
                return new HtmlResult("Пользователь отсутствует","entries");
            }
            return PartialView("_UserPartial",entry);
        }

        public ActionResult SearchFEntry()
        {
            string tag = Request.Form.Get("tagInput");
            var entry = _fservice
                .GetAllFileEntities()
                .Select(file => new ExtendedFileViewModel()
                {
                    Name = file.Name,
                    Id = file.Id.ToString(),
                    OwnerId = file.OwnerId.ToString(),
                    Created = file.Created,
                    OwnerName = _uservice.GetAllUserEntities().First(user => user.Id == file.OwnerId).UserName
                });
            if (!string.IsNullOrEmpty(tag))
            {
                entry = entry.Where(s => s.Name.Contains(tag));
            }
            if (!entry.Any())
            {
                return new HtmlResult("Файл отсутствует", "entries");
            }
            return PartialView("_FilePartial", entry);
        }

        public ActionResult ManageFiles(UserViewModel uId)
        {
            var data = _fservice
                .GetAllFileEntities()
                .OrderBy(file => file.Name)
                .Select(file => new ExtendedFileViewModel()
                    {
                        Name = file.Name,
                    Id = file.Id.ToString(),
                    OwnerId = file.OwnerId.ToString(),
                    OwnerName = _uservice.GetAllUserEntities().First(user => user.Id == file.OwnerId).UserName,
                    Created = file.Created
                });
            if (uId.UserId != null)
            {
                data = data.Where(file => file.OwnerId == uId.UserId);
            }
            return View(data);
        }

    }
}
