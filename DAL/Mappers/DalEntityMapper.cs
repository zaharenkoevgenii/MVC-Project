using System.Linq;
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
                    Files = userEntity.Files.Select(f=>f.ToOrmFile()).ToList(),
                    Profiles = userEntity.Profile.ToOrmProfile(),
                    Roles = userEntity.Roles.Select(r=>r.ToOrmRole()).ToList()
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
                    Files = ormUser.Files.Select(f=>f.ToDalFile()).ToList(),
                    Profile = ormUser.Profiles.ToDalProfile(),
                    Roles = ormUser.Roles.Select(r => r.ToDalRole()).ToList()
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
                    Private = fileEntity.Private,
                    Rating = fileEntity.Rating,
                    File = fileEntity.File,
                    ContentType = fileEntity.ContentType,
                    UserId = fileEntity.UserId,
                    Approved = fileEntity.Approved
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
                    UserId = dalFile.UserId,
                    Private = dalFile.Private,
                    Rating = dalFile.Rating,
                    File = dalFile.File,
                    ContentType = dalFile.ContentType,
                    Approved = dalFile.Approved
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
                    LastUpdateDate = profile.LastUpdateDate
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
                };
            }
            public static DalRole ToDalRole(this Roles role)
            {
                if (role == null) return null;
                return new DalRole
                {
                    Id = role.Id,
                    Name = role.Name
                };
            }
            public static Roles ToOrmRole(this DalRole dalRole)
            {
                if (dalRole == null) return null;
                return new Roles
                {
                    Id = dalRole.Id,
                    Name = dalRole.Name
                };
            }
        }
    }


