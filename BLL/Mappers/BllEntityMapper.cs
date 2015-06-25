using System.Linq;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using DAL.Interface.DTO;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            if (userEntity == null) return null;
            return new DalUser
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                Password = userEntity.Password,
                CreationTime = userEntity.CreationTime,
                Profile = ToDalProfile(userEntity.Profile),
                Roles = userEntity.Roles.Select(ToDalRole).ToList(),
                Files = userEntity.Files.Select(ToDalFile).ToList()
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null) return null;
            return new UserEntity
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Password = dalUser.Password,
                CreationTime = dalUser.CreationTime,
                Profile = ToBllProfile(dalUser.Profile),
                Roles = dalUser.Roles.Select(ToBllRole).ToList(),
                Files = dalUser.Files.Select(ToBllFile).ToList()
            };
        }

        public static DalFile ToDalFile(this FileEntity fileEntity)
        {
            if (fileEntity == null) return null;
            return new DalFile
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                CreationTime = fileEntity.CreationTime,
                UserRefId = fileEntity.UserRefId,
                Private = fileEntity.Private,
                Rating = fileEntity.Rating,
                File = fileEntity.File
            };
        }

        public static FileEntity ToBllFile(this DalFile dalFile)
        {
            if (dalFile == null) return null;
            return new FileEntity
            {
                Id = dalFile.Id,
                Name = dalFile.Name,
                CreationTime = dalFile.CreationTime,
                UserRefId = dalFile.UserRefId,
                Private = dalFile.Private,
                Rating = dalFile.Rating,
                File= dalFile.File
            };
        }
        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            if (profileEntity == null) return null;
            return new DalProfile
            {
                Id = profileEntity.Id,
                Age = profileEntity.Age,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                LastUpdateDate = profileEntity.LastUpdateDate,
                User = ToDalUser(profileEntity.User)
            };
        }
        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null) return null;
            return new ProfileEntity
            {
                Id = dalProfile.Id,
                Age = dalProfile.Age,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                LastUpdateDate = dalProfile.LastUpdateDate,
                User = ToBllUser(dalProfile.User)
            };
        }
        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            if (roleEntity == null) return null;
            return new DalRole
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                Users = roleEntity.Users.Select(ToDalUser).ToList()
            };
        }
        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;
            return new RoleEntity
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
                Users = dalRole.Users.Select(ToBllUser).ToList()
            };
        }
    }
}
