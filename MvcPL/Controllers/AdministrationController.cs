using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Filters;

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

        public ActionResult Approve()
        {
            return View("FileManage",_fservice.Get().Where(f => f.Approved == false));
        }
        public ActionResult ApproveFile(int id)
        {
            var file = _fservice.Get().First(f => f.Id == id);
            file.Approved = true;
            _fservice.Add(file);
            return RedirectToAction("Approve");
        }

        public ActionResult UserManage()
        {
            return View(_uservice.Get().Where(u=>!u.Roles.Select(r=>r.Name).Contains("admin")));
        }

        public ActionResult FileManage(int id)
        {
            return View(_fservice.Get().Where(f=>f.UserId==id));
        }
    }
}
