using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class FileService : IService<FileEntity>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<DalFile> _fileRepository;

        public FileService(IUnitOfWork uow, IRepository<DalFile> repository)
        {
            this._uow = uow;
            _fileRepository = repository;
        }

        public IEnumerable<FileEntity> Get(int n=0)
        {
            return _fileRepository.Get(n).Select(file => file.ToBllFile());
        }

        public void Add(FileEntity file)
        {
            _fileRepository.Create(file.ToDalFile());
            _uow.Commit();
        }

        public void Remove(int id)
        {
            _fileRepository.Delete(id);
            _uow.Commit();
        }


        public FileEntity Search(Expression<Func<FileEntity, bool>> f)
        {
            return _fileRepository.Get().Select(file => new FileEntity
            {
                Id = file.Id,
                CreationTime = file.CreationTime,
                File = file.File,
                Name = file.Name,
                Private = file.Private,
                Rating = file.Rating,
                UserId = file.UserId,
                ContentType = file.ContentType,
                Approved = file.Approved
            }).FirstOrDefault(f);
        }
    }
}
