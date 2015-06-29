using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
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

        public IQueryable<DalFile> Get(int n = 0)
        {
            var x = _context.Set<Files>().ToList();
            if (n != 0) x = _context.Set<Files>().Take(n).ToList();
            return x.Select(file => new DalFile
            {
                Id = file.Id,
                Name = file.Name,
                Private = file.Private,
                CreationTime = file.CreationTime,
                Rating = file.Rating,
                UserId = file.UserId,
                File = file.File,
                ContentType = file.ContentType,
                Approved = file.Approved
            }).AsQueryable();
        }

        public DalFile Search(System.Linq.Expressions.Expression<Func<DalFile, bool>> f)
        {
            return _context.Set<Files>().Select(file => new DalFile
            {
                Id = file.Id,
                Name = file.Name,
                Private = file.Private,
                CreationTime = file.CreationTime,
                Rating = file.Rating,
                UserId = file.UserId,
                File = file.File,
                ContentType = file.ContentType,
                Approved = file.Approved
            }).FirstOrDefault(f);
        }

        public void Create(DalFile file)
        {
            var f = new Files
            {
                Id = file.Id,
                Name = file.Name,
                Private = file.Private,
                CreationTime = file.CreationTime,
                Rating = file.Rating,
                UserId = file.UserId,
                File = file.File,
                ContentType = file.ContentType,
                Approved = file.Approved
            };
            _context.Set<Files>().AddOrUpdate(f);
        }

        public void Delete(int id)
        {
            var file = _context.Set<Files>().Single(f => f.Id == id);
            _context.Set<Files>().Remove(file);
        }
    }
}
