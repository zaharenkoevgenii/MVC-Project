using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<RoleEntity> _rservice;

        public CustomRoleProvider(IService<UserEntity> uservice, IService<RoleEntity> rservice)
        {
            _uservice = uservice;
            _rservice = rservice;
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            var user = _uservice.Get().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;
            var role = _uservice.Get().Select(u => u.Roles.Where(r=>r.Name== roleName)).FirstOrDefault();
            return role != null;
        }

        public override string[] GetRolesForUser(string email)
        {
                var roles = new List<string>();
                var user = _uservice.Get().FirstOrDefault(u => u.Email == email);
                if (user == null) return roles.ToArray();
                if (user.Roles == null) return roles.ToArray();
                roles.AddRange(user.Roles.Select(role => role.Name));
                return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            _rservice.Add(new RoleEntity{Name = roleName});
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