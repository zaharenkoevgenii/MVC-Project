﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers;
using System.Web.Security;
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
                .Where(file=>file.OwnerId==id)
                .Select(file => new FileViewModel()
                {
                    Id = file.Id,
                    Name = file.Name,
                    Created = file.Created
                }));
        }


        [HttpPost]
        public ActionResult Upload(FileViewModel fileView)
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
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
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_EntryControl", bllFile);
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}
