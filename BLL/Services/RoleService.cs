using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
        public class RoleService : IService<RoleEntity>
        {
            private readonly IUnitOfWork _uow;
            private readonly IRepository<DalRole> _roleRepository;

            public RoleService(IUnitOfWork uow, IRepository<DalRole> repository)
            {
                _uow = uow;
                _roleRepository = repository;
            }

            public IEnumerable<RoleEntity> Get(int n = 0)
            {
                return _roleRepository.Get(n).Select(file => file.ToBllRole());
            }

            public void Add(RoleEntity file)
            {
                _roleRepository.Create(file.ToDalRole());
                _uow.Commit();
            }

            public void Remove(int id)
            {
                _roleRepository.Delete(id);
                _uow.Commit();
            }


            public RoleEntity Search(System.Linq.Expressions.Expression<System.Func<RoleEntity, bool>> f)
            {
                return _roleRepository.Get().Select(role => new RoleEntity
                {
                    Id = role.Id,
                    Name = role.Name
                }).FirstOrDefault(f);
            }
        }
}
