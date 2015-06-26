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
                Profile = userEntity.Profile.ToDalProfile(),
                Files = userEntity.Files.Select(f=>f.ToDalFile()).ToList(),
                Roles = userEntity.Roles.Select(r=>r.ToDalRole()).ToList()
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
                Profile = dalUser.Profile.ToBllProfile(),
                Files = dalUser.Files.Select(f => f.ToBllFile()).ToList(),
                Roles = dalUser.Roles.Select(r => r.ToBllRole()).ToList()
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
                UserId = fileEntity.UserId,
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
                UserId = dalFile.UserId,
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
                LastUpdateDate = profileEntity.LastUpdateDate
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
                LastUpdateDate = dalProfile.LastUpdateDate
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            if (roleEntity == null) return null;
            return new DalRole
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }
        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;
            return new RoleEntity
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
