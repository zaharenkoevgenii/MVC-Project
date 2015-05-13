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
                            Id = user.UserId.ToString(),
                            Name = user.UserName,
                        });
        }

        public DalUser GetById(Guid key)
        {
            var ormuser = context.Set<Users>().FirstOrDefault(user => user.UserId == key);
            return new DalUser()
            {
                Id = ormuser.UserId.ToString(),
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
                UserId = Guid.Parse(e.Id),
                UserName = e.Name,
            };
            context.Set<Users>().Add(user);
        }

        public void Delete(DalUser e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
