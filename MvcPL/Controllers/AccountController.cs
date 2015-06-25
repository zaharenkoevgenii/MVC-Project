using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Infrastructura;
using MvcPL.Models;
using System.Web.Security;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [HandleAllError]
    public class AccountController : Controller
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<FileEntity> _fservice;

        public AccountController(IService<UserEntity> uservice, IService<FileEntity> fservice)
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
        public ActionResult Login(LogInViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
            {
                FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
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
            if (user.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Wrong capthca");
                return View(user);
            }
            var anyUser = _uservice.Get().Any(u => u.Email.Contains(user.Email));

            if (anyUser)
            {
                ModelState.AddModelError("Email", "Such Email already exists");
                return View(user);
            }

            if (!ModelState.IsValid) return View(user);
            MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider)
                .CreateUser(user.Email, user.Password);

            if (membershipUser != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Login(new LogInViewModel()
                {
                    Email = user.Email,
                    Password = user.Password,
                    RememberMe = false
                });
            }
            ModelState.AddModelError("", "Ошибка при регистрации");
            return View(user);
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var username = _uservice.Get().First(u => u.Id == id).Email;
            if (username == User.Identity.Name) FormsAuthentication.SignOut();
            _uservice.Remove(id);
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
    }
}
