using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "user")]
    [HandleAllError]
    public class FileWorkController : Controller
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<FileEntity> _fservice;

        public FileWorkController(IService<UserEntity> uservice, IService<FileEntity> fservice)
        {
            _fservice = fservice;
            _uservice = uservice;
        }

        public ActionResult Index()
        {
            var id = _uservice.Search(u => u.Email == User.Identity.Name).Id;
            var data = _fservice.Get()
                .Where(file => file.UserId == id)
                .OrderBy(f => f.CreationTime)
                .Select(file => new FileViewModel
                {
                    Id = file.Id.ToString(),
                    Name = file.Name,
                    Created = file.CreationTime,
                    OwnerId = file.UserId.ToString()
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
                Id = 0,
                Name = fileName,
                CreationTime = DateTime.Now,
                UserId = GetUserIdByUserName(User.Identity.Name)
            };
            _fservice.Add(bllFile);
            return PartialView("_FilePartialSimple", bllFile);
        }

        public ActionResult Download(int id)
        {
            var filename = GetFileNameByFileId(id);
            var path = Server.MapPath("~/Files/" + GetUserNameByFileId(id) + "/" + filename);
            return File(path, "*/*", filename);
        }

        public ActionResult Delete(int id)
        {
            var username = GetUserNameByFileId(id);
            System.IO.File.Delete(Server.MapPath("~/Files/" + username + "/" + GetFileNameByFileId(id)));
            _fservice.Remove(id);
            return username==User.Identity.Name ? RedirectToAction("Index") : RedirectToAction("ManageFiles", "Administration");
        }


        private string GetUserNameByFileId(int fileId)
        {
            return _uservice
                        .Get()
                        .First(u => u.Id == _fservice.Get()
                            .First(f => f.Id == fileId).UserId)
                            .Email;
        }
        private string GetFileNameByFileId(int fileId)
        {
            return _fservice.Get().First(f => f.Id == fileId).Name;
        }
        private string FileNameRemake(string fileName)
        {
            var numb = 0;
            while (_fservice.Get()
                .Where(f1 => f1.UserId == _uservice.Get()
                                                    .First(user => user.Email == User.Identity.Name)
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
        private int GetUserIdByUserName(string userName)
        {
            return _uservice.Get().First(user => user.Email == userName).Id;
        }
    }
}
