using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
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

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<Users>().Select(user => new DalUser
                        {
                            Id = user.UserId,
                            Name = user.UserName,
                        });
        }

        public IEnumerable<DalUser> GetN(int n)
        {
            return _context.Set<Users>()
                .OrderByDescending(user => user.Memberships.LastLoginDate)
                .Take(5)
                .Select(user => new DalUser
                {
                    Id = user.UserId,
                    Name = user.UserName
                });
        }

        public DalUser GetById(Guid key)
        {
            var ormuser = _context.Set<Users>().First(user => user.UserId == key);
            return new DalUser
            {
                Id = ormuser.UserId,
                Name = ormuser.UserName
            };
        }

        public DalUser GetByPredicate(System.Linq.Expressions.Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            var user = new Users
            {
                UserId = e.Id,
                UserName = e.Name,
            };
            _context.Set<Users>().Add(user);
        }

        public void Delete(Guid id)
        {
            var member = _context.Set<Memberships>().Single(m => m.UserId == id);
            _context.Set<Memberships>().Remove(member);
            var user = _context.Set<Users>().Single(o => o.UserId == id);
            _context.Set<Users>().Remove(user);
        }
    }
}
