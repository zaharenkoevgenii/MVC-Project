using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using Microsoft.Ajax.Utilities;
using MvcPL.Models;
using Ninject.Activation;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "user")]
    public class FileWorkController : Controller
    {
        private readonly IFileService _fservice;
        private readonly IUserService _uservice;
        public FileWorkController(IFileService fservice,IUserService uservice)
        {
            this._fservice = fservice;
            this._uservice = uservice;
        }

        public ActionResult Index()
        {
            var id = _uservice.GetAllUserEntities().First(user => user.UserName == User.Identity.Name).Id;
            var data = _fservice.GetN(10)
                .Where(file => file.OwnerId == id)
                .OrderBy(f => f.Created)
                .Select(file => new FileViewModel()
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
            HttpPostedFileBase file = Request.Files["fileInput"];
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                    int numb = 0;
                    while ( _fservice.GetAllFileEntities()
                        .Where(f1 => f1.OwnerId == _uservice.GetAllUserEntities().First(user => user.UserName == User.Identity.Name).Id)
                        .Count(f2 => f2.Name == fileName)!= 0)
                    {
                        numb ++;
                        string[] parts = fileName.Split('.');
                        if (numb > 1) parts[0] = parts[0].Substring(0,parts[0].Length-2-numb.ToString().Length);
                        fileName = parts[0] + "(" + numb + ").";
                        for(int i=1;i<parts.Length;i++)
                        {
                            fileName += parts[i];
                        }
                    }
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/" + User.Identity.Name));
                    file.SaveAs(Server.MapPath("~/Files/" + User.Identity.Name + "/" + fileName));
                    var bllFile = new FileEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = fileName,
                        Created = DateTime.Now,
                        OwnerId = _uservice.GetAllUserEntities().First(user=>user.UserName==User.Identity.Name).Id
                    };
                    _fservice.CreateFile(bllFile);
                    return PartialView("_EntryControl", bllFile);
                }
            return null;
        }

        public ActionResult Download(FileViewModel fileView)
        {
            var path = Server.MapPath("~/Files/" + User.Identity.Name + "/" + fileView.Name);
            return File(path, "*/*", fileView.Name);
        }

        public ActionResult Delete(string id)
        {
            var name = _fservice.GetAllFileEntities().First(f => f.Id == Guid.Parse(id)).Name;
            System.IO.File.Delete(Server.MapPath("~/Files/" + User.Identity.Name + "/" + name));
            _fservice.DeleteFile(Guid.Parse(id));
            return RedirectToAction("Index");
        }
    }
}
