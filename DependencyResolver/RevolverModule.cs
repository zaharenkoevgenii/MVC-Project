using System.Data.Entity;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
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
            Bind<IRepository<DalRole>>().To<RoleRepository>();
            Bind<IRepository<DalProfile>>().To<ProfileRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IService<UserEntity>>().To<UserService>();
            Bind<IService<FileEntity>>().To<FileService>();
            Bind<IService<ProfileEntity>>().To<ProfileService>();
            Bind<IService<RoleEntity>>().To<RoleService>();
        }
    }
}