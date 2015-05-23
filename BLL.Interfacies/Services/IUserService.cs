using System;
using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        IEnumerable<UserEntity> GetAllUserEntities();
        IEnumerable<UserEntity> GetN(int n); 
        void CreateUser(UserEntity user);
        void DeleteUser(Guid id);
    }
}