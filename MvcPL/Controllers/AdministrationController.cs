using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;
using MvcPL.Models;
using MvcPL.Utils;

namespace MvcPL.Controllers
{
    [HandleAllError]
    public class AdministrationController : Controller
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<FileEntity> _fservice;

        public AdministrationController(IService<UserEntity> uservice, IService<FileEntity> fservice)
        {
            _fservice = fservice;
            _uservice = uservice;
        }

        public ActionResult Index()
        {
            return View(_uservice
                .Get()
                .OrderBy(user => user.Email)
                .Select(user => new UserViewModel
                {
                    Email = user.Email,
                    Id = user.Id
                }));
        }

        public ActionResult SearchUEntry()
        {
            string tag = Request.Form.Get("tagInput");
            var entry=_uservice
                .Get()
                .Select(user => new UserViewModel
                {
                      Email = user.Email,
                      Id = user.Id
                    });
            if (!string.IsNullOrEmpty(tag))
            {
                entry = entry.Where(s => s.Email.Contains(tag));
            }
            if (!entry.Any())
            {
                return new HtmlResult("Пользователь отсутствует","entries");
            }
            return PartialView("_UserPartial",entry);
        }

        public ActionResult SearchFEntry()
        {
            var tag = Request.Form.Get("tagInput");
            var entry = _fservice
                .Get()
                .Select(file => new ExtendedFileViewModel
                {
                    Name = file.Name,
                    Id = file.Id.ToString(CultureInfo.InvariantCulture),
                    OwnerId = file.UserRefId.ToString(CultureInfo.InvariantCulture),
                    Created = file.CreationTime,
                    OwnerName = _uservice.Get().First(user => user.Id == file.UserRefId).Email
                });
            if (!string.IsNullOrEmpty(tag))
            {
                entry = entry.Where(s => s.Name.Contains(tag));
            }
            if (!entry.Any())
            {
                return new HtmlResult("Файл отсутствует", "entries");
            }
            return PartialView("_FilePartial", entry);
        }

        public ActionResult ManageFiles(string id)
        {
            var data = _fservice
                .Get()
                .OrderBy(file => file.Name)
                .Select(file => new ExtendedFileViewModel
                {
                    Name = file.Name,
                    Id = file.Id.ToString(CultureInfo.InvariantCulture),
                    OwnerId = file.UserRefId.ToString(CultureInfo.InvariantCulture),
                    OwnerName = _uservice.Get().First(user => user.Id == file.UserRefId).Email,
                    Created = file.CreationTime
                });
            if (id != null) data = data.Where(file => file.OwnerId == id);
            return View(data);
        }

    }
}
