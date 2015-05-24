using System;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Models;
using System.Web.Security;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _uservice;
        private readonly IFileService _fservice;


        public AccountController(IUserService uservice,IFileService fservice)
        {
            _uservice = uservice;
            _fservice = fservice;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("wrong user info", "Неправильный пароль или логин");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegistrationViewModel user)
        {
            Membership.CreateUser(user.UserName, user.Password, user.Email);
            Roles.AddUserToRole(user.UserName, "user");
            if (user.UserName == "admin") Roles.AddUserToRole(user.UserName, "admin");
            FormsAuthentication.SetAuthCookie(user.UserName, user.LogInNow);
            return RedirectToAction("Index","Home");
        }
        
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var files=_fservice.GetAllFileEntities().Where(f => f.OwnerId == Guid.Parse(id));
            var username = _uservice.GetAllUserEntities().First(u => u.Id == Guid.Parse(id)).UserName;
            foreach (var file in files)
            {
                RedirectToAction("Delete", "FileWork", file.Id);
                System.IO.File.Delete(Server.MapPath("~/Files/" + username + "/" + file.Name));
            }
            if (Directory.Exists(Server.MapPath("~/Files/" + username)))
                Directory.Delete(Server.MapPath("~/Files/" + username));
            if (username == User.Identity.Name)
                FormsAuthentication.SignOut();
            _uservice.DeleteUser(Guid.Parse(id));
            return RedirectToAction("Index","Administration");
        }
    }
}
