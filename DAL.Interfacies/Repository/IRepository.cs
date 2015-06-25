using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(int n=0);
        TEntity Search(Expression<Func<TEntity, bool>> f);
        void Create(TEntity entity);
        void Delete(int id);
    }
}