using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
    public class UserService : IService<UserEntity>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalUser> _userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            _uow = uow;
            _userRepository = repository;
        }

        public IEnumerable<UserEntity> Get(int n=0)
        {
            return _userRepository.Get(n).Select(user => user.ToBllUser());
        }

        public void Add(UserEntity user)
        {
            _userRepository.Create(user.ToDalUser());
            _uow.Commit();
        }

        public void Remove(int id)
        {
            _userRepository.Delete(id);
            _uow.Commit();
        }


        public IEnumerable<UserEntity> Search(System.Linq.Expressions.Expression<Func<UserEntity, bool>> f)
        {
            return _userRepository.Get().Select(user => user.ToBllUser()).Where(f);
        }
    }
}
