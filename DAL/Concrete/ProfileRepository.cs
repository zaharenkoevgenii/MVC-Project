using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class ProfileRepository : IRepository<DalProfile>
    {
        private readonly DbContext _context;

        public ProfileRepository(DbContext uow)
        {
            _context = uow;
        }
        public IQueryable<DalProfile> Get(int n = 0)
        {
            var x = _context.Set<Profiles>().ToList();
            if (n != 0) x = _context.Set<Profiles>().Take(n).ToList();
            return x.Select(profile => new DalProfile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                LastUpdateDate = profile.LastUpdateDate
            }).AsQueryable();
        }
        public DalProfile Search(System.Linq.Expressions.Expression<Func<DalProfile, bool>> f)
        {
            return _context.Set<Profiles>().Select(profile => new DalProfile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                LastUpdateDate = profile.LastUpdateDate,
            }).FirstOrDefault(f);
        }
        public void Create(DalProfile entity)
        {
            var profile = new Profiles
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age,
                LastUpdateDate = entity.LastUpdateDate,
                Users = _context.Set<Users>().First(u=>u.Id==entity.Id)
            };
            _context.Set<Profiles>().Add(profile);
        }
        public void Delete(int id)
        {
            var profile = _context.Set<Profiles>().Single(p => p.Id == id);
            _context.Set<Profiles>().Remove(profile);
        }
    }
}
