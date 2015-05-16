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
    public class FileService : IFileService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalFile> fileRepository;

        public FileService(IUnitOfWork uow, IRepository<DalFile> repository)
        {
            this.uow = uow;
            this.fileRepository = repository;
        }

        public IEnumerable<FileEntity> GetAllFileEntities()
        {
                return fileRepository.GetAll().Select(user => user.ToBllFile());
        }

        public void CreateFile(FileEntity file)
        {
            fileRepository.Create(file.ToDalFile());
            uow.Commit();
        }


        public void DeleteFile(FileEntity file)
        {
            fileRepository.Delete(file.ToDalFile());
            uow.Commit();
        }
    }
}
