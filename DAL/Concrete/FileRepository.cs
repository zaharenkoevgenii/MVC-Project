using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class FileRepository : IRepository<DalFile>
    {
        private readonly DbContext _context;

        public FileRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalFile> GetAll()
        {
            return _context.Set<Files>().Select(file => new DalFile
            {
                Id = file.FileId,
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId
            });
        }

        public IEnumerable<DalFile> GetN(int n)
        {
            return _context.Set<Files>()
                .OrderByDescending(file=>file.CreationTime)
                .Take(5)
                .Select(file => new DalFile
            {
                Id = file.FileId,
                Name = file.FileName,
                Created = file.CreationTime,
                OwnerId = file.UserId
            });
        }

        public DalFile GetById(Guid key)
        {
            var ormFile = _context.Set<Files>().First(file => file.FileId == key);
            return new DalFile
            {
                Id = ormFile.FileId,
                Name = ormFile.FileName,
                Created = ormFile.CreationTime,
                OwnerId = ormFile.UserId
            };
        }

        public DalFile GetByPredicate(System.Linq.Expressions.Expression<Func<DalFile, bool>> f)
        {
            var ormFile = _context.Set<Files>().Select(file => new DalFile
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
            var file = new Files
            {
                FileId = e.Id,
                FileName = e.Name,
                CreationTime = e.Created,
                UserId = e.OwnerId
            };
            _context.Set<Files>().Add(file);
        }

        public void Delete(Guid id)
        {
            var file = _context.Set<Files>().Single(o => o.FileId == id);
            _context.Set<Files>().Remove(file);
        }
    }
}
