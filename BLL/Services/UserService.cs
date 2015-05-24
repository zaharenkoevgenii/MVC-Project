using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalUser> _userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            _uow = uow;
            _userRepository = repository;
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
                return _userRepository.GetAll().Select(user => user.ToBllUser()); 
        }

        public IEnumerable<UserEntity> GetN(int n)
        {
            return _userRepository.GetN(n).Select(user => user.ToBllUser());
        }

        public void CreateUser(UserEntity user)
        {
            _userRepository.Create(user.ToDalUser());
            _uow.Commit();
        }

        public void DeleteUser(Guid id)
        {
            _userRepository.Delete(id);
            _uow.Commit();
        }
    }
}
