using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using Ninject.Modules;
using ORM;

namespace DependencyResolver
{
    public class RevolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EntityModel>().InSingletonScope();
            Bind<IRepository<DalUser>>().To<UserRepository>();
            Bind<IRepository<DalFile>>().To<FileRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<IFileService>().To<FileService>();
        }
    }
}