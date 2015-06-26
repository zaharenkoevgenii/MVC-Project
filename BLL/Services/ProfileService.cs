﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
        public class ProfileService : IService<ProfileEntity>
        {
            private readonly IUnitOfWork _uow;
            private readonly IRepository<DalProfile> _profileRepository;

            public ProfileService(IUnitOfWork uow, IRepository<DalProfile> repository)
            {
                _uow = uow;
                _profileRepository = repository;
            }

            public IEnumerable<ProfileEntity> Get(int n = 0)
            {
                return _profileRepository.Get(n).Select(file => file.ToBllProfile());
            }

            public void Add(ProfileEntity file)
            {
                _profileRepository.Create(file.ToDalProfile());
                _uow.Commit();
            }

            public void Remove(int id)
            {
                _profileRepository.Delete(id);
                _uow.Commit();
            }


            public ProfileEntity Search(System.Linq.Expressions.Expression<Func<ProfileEntity, bool>> f)
            {
                return _profileRepository.Get().Select(profile => new ProfileEntity
                {
                    Id = profile.Id,
                    Age = profile.Age,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    LastUpdateDate = profile.LastUpdateDate,
                }).FirstOrDefault(f);
            }
        }
    }
