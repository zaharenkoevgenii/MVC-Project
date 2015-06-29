using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;

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

        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files["fileInput"];
            if (file == null || file.ContentLength <= 0 || string.IsNullOrEmpty(file.FileName)) return null;
            var fileName = FileNameRemake(file.FileName);
            var binaryReader = new BinaryReader(file.InputStream);
                var bllFile = new FileEntity
                {
                    Name = fileName,
                    CreationTime = DateTime.Now,
                    UserId = _uservice.Get().First(u=>u.Email==User.Identity.Name).Id,
                    File =binaryReader.ReadBytes(file.ContentLength),
                    Private = true,
                    Rating = 0,
                    ContentType = file.ContentType,
                    Approved = true
                };
            binaryReader.Dispose();
            _fservice.Add(bllFile);
            bllFile = _fservice.Get().First(f => f.Name == fileName);
            return PartialView("_FilePartialSimple", bllFile);
        }

        public ActionResult Download(int id)
        {
            var file = _fservice.Get().FirstOrDefault(f => f.Id == id);
            if (file == null) return RedirectToAction("Index", "Profile");
            var rFile = new FileContentResult(file.File, file.ContentType) {FileDownloadName = file.Name};
            return rFile;
        }

        public ActionResult Delete(int id)
        {
            var username = _uservice.Get().First(u => u.Id == _fservice.Get().First(f => f.Id == id).UserId).Email;
            _fservice.Remove(id);
            return username == User.Identity.Name ? RedirectToAction("Index", "Profile") : RedirectToAction("FileManage", "Administration");
        }

        private string FileNameRemake(string fileName)
        {
            var numb = 0;
            var parts = fileName.Split('.');
            if (parts[0].Length > 30)
                parts[0] = parts[0].Substring(0, 25) + "..." + parts[0].Substring(parts[0].Length - 2, 2);
            fileName = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                fileName += "." + parts[i];
            }
            while (_fservice.Get()
                .Where(f1 => f1.UserId == _uservice.Get()
                                                    .First(user => user.Email == User.Identity.Name)
                                                    .Id)
                .Count(f2 => f2.Name == fileName) != 0)
            {
                numb++;
                fileName = parts[0] + "(" + numb + ")";
                for (int i = 1; i < parts.Length; i++)
                {
                    fileName += "."+parts[i];
                }
            }
            return fileName;
        }

        public ActionResult Privacy(int id)
        {
            var file = _fservice.Get().First(f => f.Id == id);
            file.Private = !file.Private;
            if(!file.Private) file.Approved = false;
            _fservice.Add(file);
            return RedirectToAction("Index", "Profile");
        }
    }
}
