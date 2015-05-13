using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Models;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects.DataClasses;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _service;
        public AccountController(IUserService service)
        {
            this._service = service;
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
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
            var blluser = new UserEntity()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Password = user.Password,
                Email  = user.Email
            };
            Membership.CreateUser(blluser.UserName, blluser.Password,blluser.Email);
            Roles.AddUserToRole(blluser.UserName, "user");
            if (blluser.UserName=="admin")
                Roles.AddUserToRole(blluser.UserName,"admin");
            FormsAuthentication.SetAuthCookie(user.UserName, user.LogInNow);
            return RedirectToAction("Index","Home");
        }
    }
}
