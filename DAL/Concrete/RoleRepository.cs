using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRepository<DalRole>
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext uow)
        {
            _context = uow;
        }
        public IEnumerable<DalRole> Get(int n = 0)
        {
            var x = _context.Set<Roles>().ToList();
            if (n != 0) x = _context.Set<Roles>().Take(n).ToList();
            return x.Select(role => new DalRole
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users.Select(u=>u.ToDalUser()).ToList()
            });
        }
        public DalRole Search(System.Linq.Expressions.Expression<Func<DalRole, bool>> f)
        {
            return _context.Set<Roles>().Select(role => new DalRole
            {
                Id = role.Id,
                Name = role.Name,
                Users = role.Users.Select(u=>u.ToDalUser()).ToList()
            }).FirstOrDefault(f);
        }
        public void Create(DalRole entity)
        {
            var role = new Roles
            {
                Id = entity.Id,
                Name = entity.Name,
                Users = entity.Users.Select(u=>u.ToOrmUser()).ToList()
            };
            _context.Set<Roles>().Add(role);
        }
        public void Delete(int id)
        {
            var role = _context.Set<Roles>().Single(r => r.Id == id);
            _context.Set<Roles>().Remove(role);
        }
    }
}
