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
using MvcPL.Models;

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
            return View(_fservice.GetAllFileEntities()
                .OrderBy(f=>f.Created)
                .Where(file=>file.OwnerId==id)
                .Select(file => new FileViewModel()
                {
                    Id = file.Id,
                    Name = file.Name,
                    Created = file.Created
                }));
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(FileViewModel fileView)
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
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
                    upload.SaveAs(Server.MapPath("~/Files/"+ User.Identity.Name+"/"+ fileName));
                    var bllFile = new FileEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = fileName,
                        Created = DateTime.Now,
                        OwnerId = _uservice.GetAllUserEntities().First(user=>user.UserName==User.Identity.Name).Id
                    };
                    _fservice.CreateFile(bllFile);
                }
            }
            return Json("File added succesfully.");
        }

    }
}
