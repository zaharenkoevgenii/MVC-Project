using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Filters;
using MvcPL.Infrastructura;
using MvcPL.Models;
using System.Web.Security;

namespace MvcPL.Controllers
{
    [HandleAllError]
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
            if (!FindUser(model.UserName))
            {
                if (!ModelState.IsValid) return View(model);
                if (!Membership.ValidateUser(model.UserName, model.Password)) return View(model);
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Home");
            }
                return RedirectToAction("Create", "Account");
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
            if (!FindUser(user.UserName))
            {
                return Login(new LogInViewModel() {Password = user.Password, UserName = user.UserName});
            }

            if (user.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
                return View(user);
            }

            var anyUser = _uservice.GetAllUserEntities().Any(u => u.UserName.Contains(user.UserName));

            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким именем уже зарегистрирован");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                Membership.CreateUser(user.UserName, user.Password, user.Email);
                Roles.AddUserToRole(user.UserName, "user");
                if (user.UserName == "admin") Roles.AddUserToRole(user.UserName, "admin");
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
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

        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            Response.Clear();
            Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
            return null;
        }

        private bool FindUser(string name)
        {
            return _uservice.GetAllUserEntities().FirstOrDefault(u => u.UserName == name) == null;
        }
    }
}
