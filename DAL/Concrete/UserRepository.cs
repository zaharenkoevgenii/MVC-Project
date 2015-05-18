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
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<Users>().Select(user => new DalUser()
                        {
                            Id = user.UserId,
                            Name = user.UserName,
                        });
        }

        public DalUser GetById(Guid key)
        {
            var ormuser = context.Set<Users>().FirstOrDefault(user => user.UserId == key);
            return new DalUser()
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
            var user = new Users()
            {
                UserId = e.Id,
                UserName = e.Name,
            };
            context.Set<Users>().Add(user);
        }

        public void Delete(Guid id)
        {
            var member = context.Set<Memberships>().Single(m => m.UserId == id);
            context.Set<Memberships>().Remove(member);
            var user = context.Set<Users>().Single(o => o.UserId == id);
            context.Set<Users>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
