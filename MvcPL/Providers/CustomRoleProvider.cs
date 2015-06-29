using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using DependencyResolver;
using Ninject;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private IKernel _kernel;

        public override bool IsUserInRole(string email, string roleName)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var uservice = _kernel.Get<IService<UserEntity>>();
            var user = uservice.Get().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;
            var role = user.Roles.Select(r => r.Name).Contains(roleName);
            return role;
        }

        public override string[] GetRolesForUser(string email)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var uservice = _kernel.Get<IService<UserEntity>>();
                var roles = new List<string>();
                var user = uservice.Get().FirstOrDefault(u => u.Email == email);
                if (user == null) return roles.ToArray();
                if (user.Roles == null) return roles.ToArray();
                roles.AddRange(user.Roles.Select(role => role.Name));
                return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var rservice = _kernel.Get<IService<RoleEntity>>();
            rservice.Add(new RoleEntity{Name = roleName});
        }

        #region Stabs
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
        #endregion
    }
}