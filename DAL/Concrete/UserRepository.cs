using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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

        public IQueryable<DalUser> Get(int n=0)
        {
            var x = _context.Set<Users>().ToList();
            if (n != 0) x = _context.Set<Users>().Take(n).ToList();
            return x.Select(user => new DalUser
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Password = user.Password,
                            CreationTime = user.CreationTime,
                            Files = user.Files.Select(f=>f.ToDalFile()).ToList(),
                            Roles = user.Roles.Select(r=>r.ToDalRole()).ToList(),
                            Profile = user.Profiles.ToDalProfile()
                        }).AsQueryable();
        }

        public void Create(DalUser user)
        {
            var u = new Users
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreationTime = user.CreationTime,
                Profiles = user.Profile.ToOrmProfile(),
                Files = user.Files.Select(f=>f.ToOrmFile()).ToList(),
                Roles = user.Roles.Select(r=>r.ToOrmRole()).ToList()
            };
            _context.Set<Users>().AddOrUpdate(u);
        }

        public void Delete(int id)
        {
            var user = _context.Set<Users>().Single(u => u.Id == id);
            _context.Set<Users>().Remove(user);
        }
    }
}
