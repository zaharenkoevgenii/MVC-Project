using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Profile;
using BLL.Interface.Entities;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    public class CustomProfileProvider : ProfileProvider
    {
        private readonly IService<UserEntity> _uservice;
        private readonly IService<ProfileEntity> _pservice;

        public CustomProfileProvider(IService<UserEntity> uservice, IService<ProfileEntity> pservice)
        {
            _uservice = uservice;
            _pservice = pservice;
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();
            if (collection.Count < 1)
            {
                return result;
            }
            var username = (string)context["UserName"];
            if (String.IsNullOrEmpty(username)) return result;
            var user = _uservice.Get().FirstOrDefault(u => u.Email.Equals(username));
            if (user == null) return result;
            var profile = user.Profile;
            if (profile != null)
            {
                foreach (var spv in from SettingsProperty prop in collection select new SettingsPropertyValue(prop)
                {PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null)})
                {
                    result.Add(spv);
                }
            }
            else
            {
                foreach (var svp in from SettingsProperty prop in collection select new SettingsPropertyValue(prop) 
                { PropertyValue = null })
                {
                    result.Add(svp);
                }
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            var username = (string)context["UserName"];

            if (string.IsNullOrEmpty(username) || collection.Count < 1)
                return;
            var user = _uservice.Get().FirstOrDefault(u => u.Email.Equals(username));
            if (user == null) return;
            var profile = user.Profile;
            if (profile != null)
            {
                foreach (SettingsPropertyValue val in collection)
                {
                    profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                }
                profile.LastUpdateDate = DateTime.Now;
            }
            else
            {
                profile = new ProfileEntity();
                foreach (SettingsPropertyValue val in collection)
                {
                    profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                }
                profile.LastUpdateDate = DateTime.Now;
                profile.User = user;
                _pservice.Add(profile);
            }
        }
        #region Stabs
        public override string ApplicationName { get; set; }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption,
            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption,
            string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}