using System;
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
                Id = file.FileId.ToString(),
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId.ToString()
            });
        }

        public DalFile GetById(Guid key)
        {
            var ormFile = context.Set<Files>().First(file => file.FileId == key);
            return new DalFile()
            {
                Id = ormFile.FileId.ToString(),
                Name = ormFile.FileName,
                Created = ormFile.CreationTime,
                OwnerId = ormFile.UserId.ToString()
            };
        }

        public DalFile GetByPredicate(System.Linq.Expressions.Expression<Func<DalFile, bool>> f)
        {
            var ormFile = context.Set<Files>().Select(file => new DalFile()
            {
                Id = file.FileId.ToString(),
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId.ToString()
            }).FirstOrDefault(f);
            return ormFile;
        }

        public void Create(DalFile e)
        {
            var file = new Files()
            {
                FileId = Guid.Parse(e.Id),
                FileName = e.Name,
                CreationTime = e.Created,
                UserId = Guid.Parse(e.OwnerId)
            };
            context.Set<Files>().Add(file);
        }

        public void Delete(DalFile e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalFile entity)
        {
            throw new NotImplementedException();
        }
    }
}
