using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Models;

namespace MvcPL.Controllers
{
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
            if (user.Profile == null)
                return View(new ProfileViewModel
                {
                    Age = 0,
                    FirstName = "Noname",
                    LastName = "Noname"
                });
            return View(new ProfileViewModel
            {
                Age=user.Profile.Age,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
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
