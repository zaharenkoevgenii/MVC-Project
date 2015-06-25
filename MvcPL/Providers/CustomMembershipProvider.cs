//Select Assemblies - > Extensions -> System.Web.Helpers

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Providers.Entities;
using System.Web.Security;
using BLL.Interface.Entities;
using System.Web.Helpers;
using BLL.Interfacies.Services;
using DependencyResolver;
using MvcPL.Infrastructura;
using Ninject;
using RoleEntity = BLL.Interfacies.Entities.RoleEntity;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private  IKernel _kernel;

        public MembershipUser CreateUser(string email, string password)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var uservice = _kernel.Get<IService<UserEntity>>();
            var rservice = _kernel.Get<IService<RoleEntity>>();
            var membershipUser = GetUser(email, false);
            if (membershipUser != null) return null;
                var user = new UserEntity
                {
                    Email = email,
                    Password = Crypto.HashPassword(password),
                    CreationTime = DateTime.Now,
                    Files = new List<FileEntity>(),
                    Roles = new List<RoleEntity>()
                };
            var role = rservice.Get().FirstOrDefault(r => r.Name == "user");
                if (role != null)
                {
                    user.Roles.Add(role);
                }
            uservice.Add(user);
            membershipUser = GetUser(email, false);
            return membershipUser;

        }

        public override bool ValidateUser(string email, string password)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var uservice = _kernel.Get<IService<UserEntity>>();
                var user = uservice.Get().FirstOrDefault(u => u.Email == email);
                return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            _kernel = new StandardKernel(new RevolverModule());
            var uservice = _kernel.Get<IService<UserEntity>>();
                var user = uservice.Get().FirstOrDefault(u => u.Email==email);
                if (user == null) return null;
                var memberUser = new MembershipUser("CustomMembershipProvider", user.Email,
                    null, null, null, null,
                    false, false, user.CreationTime, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                return memberUser;
        }

        #region Stabs
        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}