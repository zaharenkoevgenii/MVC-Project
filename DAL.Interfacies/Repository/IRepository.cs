using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetN(int n); 
        TEntity GetById(Guid key);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        void Create(TEntity entity);
        void Delete(Guid id);
        void Update(TEntity entity);
    }
}