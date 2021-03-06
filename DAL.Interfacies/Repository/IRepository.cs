﻿using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Get(int n=0);
        void Create(TEntity entity);
        void Delete(int id);
    }
}