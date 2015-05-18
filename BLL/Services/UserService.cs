using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            this.uow = uow;
            this.userRepository = repository;
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
                return userRepository.GetAll().Select(user => user.ToBllUser()); 
        }

        public void CreateUser(UserEntity user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(Guid id)
        {
            userRepository.Delete(id);
            uow.Commit();
        }
    }
}
