using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext _context;

        public UserRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalUser> Get(int n=0)
        {
            var x = _context.Set<Users>().ToList();
            if (n != 0) x = _context.Set<Users>().Take(n).ToList();
            return x.Select(user => new DalUser
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Password = user.Password,
                            CreationTime = user.CreationTime,
                            Files = new List<DalFile>(),
                            Profile = user.Profiles.ToDalProfile(),
                            Roles = new List<DalRole>()
                        });
        }

        public DalUser Search(System.Linq.Expressions.Expression<Func<DalUser, bool>> f)
        {
            return _context.Set<Users>().Select(user => new DalUser
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationTime = user.CreationTime
            }).FirstOrDefault(f);
        }

        public void Create(DalUser user)
        {
            var u = new Users
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationTime = user.CreationTime
            };
            _context.Set<Users>().Add(u);
        }

        public void Delete(int id)
        {
            var user = _context.Set<Users>().Single(u => u.Id == id);
            _context.Set<Users>().Remove(user);
        }
    }
}
