﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class FileRepository : IRepository<DalFile>
    {
        private readonly DbContext context;

        public FileRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalFile> GetAll()
        {
            return context.Set<Files>().Select(file => new DalFile()
            {
                Id = file.FileId,
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId
            });
        }

        public DalFile GetById(Guid key)
        {
            var ormFile = context.Set<Files>().First(file => file.FileId == key);
            return new DalFile()
            {
                Id = ormFile.FileId,
                Name = ormFile.FileName,
                Created = ormFile.CreationTime,
                OwnerId = ormFile.UserId
            };
        }

        public DalFile GetByPredicate(System.Linq.Expressions.Expression<Func<DalFile, bool>> f)
        {
            var ormFile = context.Set<Files>().Select(file => new DalFile()
            {
                Id = file.FileId,
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId
            }).FirstOrDefault(f);
            return ormFile;
        }

        public void Create(DalFile e)
        {
            var file = new Files()
            {
                FileId = e.Id,
                FileName = e.Name,
                CreationTime = e.Created,
                UserId = e.OwnerId
            };
            context.Set<Files>().Add(file);
        }

        public void Delete(Guid id)
        {
            var file = context.Set<Files>().Single(o => o.FileId == id);
            context.Set<Files>().Remove(file);
        }

        public void Update(DalFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
