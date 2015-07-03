using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "user")]
    [HandleAllError]
    public class ProfileController : Controller
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<ProfileEntity> _pservice;
        public ProfileController(IService<UserEntity> service, IService<ProfileEntity> pservice)
        {
            _uservice = service;
            _pservice = pservice;
        }
        public ActionResult Index()
        {
            var user = _uservice.Get().First(u => u.Email == User.Identity.Name);
                user.Profile = new ProfileEntity
                {
                    Age = user.Profile.Age,
                    FirstName = user.Profile.FirstName,
                    LastName = user.Profile.LastName,
                    LastUpdateDate = user.Profile.LastUpdateDate
                };
                return View(new UserEntity
                {
                    Email = user.Email,
                    Files = user.Files,
                    Id = user.Id,
                    Profile = user.Profile,
                    Roles = user.Roles
                });
        }
        [HttpGet]
        public ActionResult FillProfile()
        {
            return View("FillProfile");
        }
        [HttpPost]
        public ActionResult FillProfile(ProfileViewModel profile)
        {
            var user = _uservice.Get().First(u => u.Email == User.Identity.Name);
            user.Profile = new ProfileEntity
            {
                Age = profile.Age,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Id = user.Id,
                LastUpdateDate = DateTime.Now
            };
            _pservice.Add(user.Profile);
            _uservice.Add(user);
            return RedirectToAction("Index");
        }
    }
}
