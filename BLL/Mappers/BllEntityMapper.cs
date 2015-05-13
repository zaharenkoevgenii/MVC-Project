using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMapper
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.UserName,
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                UserName = dalUser.Name,
            };
        }

        public static DalFile ToDalFile(this FileEntity fileEntity)
        {
            return new DalFile()
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                Created = fileEntity.Created,
                OwnerId = fileEntity.OwnerId
            };
        }

        public static FileEntity ToBllFile(this DalFile dalFile)
        {
            return new FileEntity()
            {
                Id = dalFile.Id,
                Name = dalFile.Name,
                Created = dalFile.Created,
                OwnerId = dalFile.OwnerId
            };
        }
    }
}
