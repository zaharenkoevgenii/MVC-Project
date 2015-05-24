using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Filters;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "user")]
    [HandleAllError]
    public class FileWorkController : Controller
    {
        private readonly IFileService _fservice;
        private readonly IUserService _uservice;


        public FileWorkController(IFileService fservice,IUserService uservice)
        {
            _fservice = fservice;
            _uservice = uservice;
        }

        public ActionResult Index()
        {
            var id = GetUserIdByUserName(User.Identity.Name);
            var data = _fservice.GetAllFileEntities()
                .Where(file => file.OwnerId == id)
                .OrderBy(f => f.Created)
                .Select(file => new FileViewModel
                {
                    Id = file.Id.ToString(),
                    Name = file.Name,
                    Created = file.Created,
                    OwnerId = file.OwnerId.ToString()
                });
            return View(data);
        }
        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files["fileInput"];
            if (file == null || file.ContentLength <= 0 || string.IsNullOrEmpty(file.FileName)) return null;
            var fileName = FileNameRemake(file.FileName);
            Directory.CreateDirectory(Server.MapPath("~/Files/" + User.Identity.Name));
            file.SaveAs(Server.MapPath("~/Files/" + User.Identity.Name + "/" + fileName));
            var bllFile = new FileEntity
            {
                Id = Guid.NewGuid(),
                Name = fileName,
                Created = DateTime.Now,
                OwnerId = GetUserIdByUserName(User.Identity.Name)
            };
            _fservice.CreateFile(bllFile);
            return PartialView("_FilePartialSimple", bllFile);
        }

        public ActionResult Download(string id)
        {
            var filename = GetFileNameByFileId(Guid.Parse(id));
            var path = Server.MapPath("~/Files/" + GetUserNameByFileId(Guid.Parse(id)) + "/" + filename);
            return File(path, "*/*", filename);
        }

        public ActionResult Delete(string id)
        {
            var username = GetUserNameByFileId(Guid.Parse(id));
            System.IO.File.Delete(Server.MapPath("~/Files/" + username + "/" + GetFileNameByFileId(Guid.Parse(id))));
            _fservice.DeleteFile(Guid.Parse(id));
            return username==User.Identity.Name ? RedirectToAction("Index") : RedirectToAction("ManageFiles", "Administration");
        }


        private string GetUserNameByFileId(Guid fileId)
        {
            return _uservice
                        .GetAllUserEntities()
                        .First(u => u.Id == _fservice.GetAllFileEntities()
                            .First(f => f.Id == fileId).OwnerId)
                            .UserName;
        }
        private string GetFileNameByFileId(Guid fileId)
        {
            return _fservice.GetAllFileEntities().First(f => f.Id == fileId).Name;
        }
        private string FileNameRemake(string fileName)
        {
            var numb = 0;
            while (_fservice.GetAllFileEntities()
                .Where(f1 => f1.OwnerId == _uservice.GetAllUserEntities()
                                                    .First(user => user.UserName == User.Identity.Name)
                                                    .Id)
                .Count(f2 => f2.Name == fileName) != 0)
            {
                numb++;
                var parts = fileName.Split('.');
                if (numb > 1) parts[0] = parts[0].Substring(0, parts[0].Length - 2 - numb.ToString(CultureInfo.InvariantCulture).Length);
                fileName = parts[0] + "(" + numb + ").";
                for (int i = 1; i < parts.Length; i++)
                {
                    fileName += parts[i];
                }
            }
            return fileName;
        }
        private Guid GetUserIdByUserName(string userName)
        {
            return _uservice.GetAllUserEntities().First(user => user.UserName == userName).Id;
        }
    }
}
