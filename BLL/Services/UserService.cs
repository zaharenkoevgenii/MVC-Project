using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

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


        public UserEntity Search(System.Linq.Expressions.Expression<Func<UserEntity, bool>> f)
        {
            return _userRepository.Get().Select(user => new UserEntity()
            {
                Id = user.Id,
                CreationTime = user.CreationTime,
                Email = user.Email,
                Password = user.Password,
            }).FirstOrDefault(f);
        }
    }
}
