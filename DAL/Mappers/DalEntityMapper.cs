using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;
using ORM;

namespace DAL.Mappers
{
        public static class DalEntityMapper
        {
            public static Users ToOrmUser(this DalUser userEntity)
            {
                if (userEntity == null) return null;
                return new Users
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    CreationTime = userEntity.CreationTime,
                    Profiles = ToOrmProfile(userEntity.Profile),
                    Roles = userEntity.Roles.Select(ToOrmRole).ToList(),
                    Files = userEntity.Files.Select(ToOrmFile).ToList()
                };
            }
            public static DalUser ToDalUser(this Users ormUser)
            {
                if (ormUser == null) return null;
                return new DalUser
                {
                    Id = ormUser.Id,
                    Email = ormUser.Email,
                    Password = ormUser.Password,
                    CreationTime = ormUser.CreationTime,
                    Profile = ToDalProfile(ormUser.Profiles),
                    Roles = ormUser.Roles.Select(ToDalRole).ToList(),
                    Files = ormUser.Files.Select(ToDalFile).ToList()
                };
            }
            public static DalFile ToDalFile(this Files fileEntity)
            {
                if (fileEntity == null) return null;
                return new DalFile
                {
                    Id = fileEntity.Id,
                    Name = fileEntity.Name,
                    CreationTime = fileEntity.CreationTime,
                    UserRefId = fileEntity.UserId,
                    Private = fileEntity.Private,
                    Rating = fileEntity.Rating,
                    File = fileEntity.File
                };
            }
            public static Files ToOrmFile(this DalFile dalFile)
            {
                if (dalFile == null) return null;
                return new Files
                {
                    Id = dalFile.Id,
                    Name = dalFile.Name,
                    CreationTime = dalFile.CreationTime,
                    UserId = dalFile.UserRefId,
                    Private = dalFile.Private,
                    Rating = dalFile.Rating,
                    File = dalFile.File
                };
            }
            public static DalProfile ToDalProfile(this Profiles profile)
            {
                if (profile == null) return null;
                return new DalProfile
                {
                    Id = profile.Id,
                    Age = profile.Age,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    LastUpdateDate = profile.LastUpdateDate,
                    User = ToDalUser(profile.Users)
                };
            }
            public static Profiles ToOrmProfile(this DalProfile dalProfile)
            {
                if (dalProfile == null) return null;
                return new Profiles
                {
                    Id = dalProfile.Id,
                    Age = dalProfile.Age,
                    FirstName = dalProfile.FirstName,
                    LastName = dalProfile.LastName,
                    LastUpdateDate = dalProfile.LastUpdateDate,
                    Users = ToOrmUser(dalProfile.User)
                };
            }
            public static DalRole ToDalRole(this Roles role)
            {
                if (role == null) return null;
                return new DalRole
                {
                    Id = role.Id,
                    Name = role.Name,
                    Users = role.Users.Select(ToDalUser).ToList()
                };
            }
            public static Roles ToOrmRole(this DalRole dalRole)
            {
                if (dalRole == null) return null;
                return new Roles
                {
                    Id = dalRole.Id,
                    Name = dalRole.Name,
                    Users = dalRole.Users.Select(ToOrmUser).ToList()
                };
            }
        }
    }


