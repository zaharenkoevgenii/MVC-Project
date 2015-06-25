using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
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
    }
}
