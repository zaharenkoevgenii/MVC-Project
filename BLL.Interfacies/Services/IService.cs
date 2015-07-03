using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(int n = 0);
        void Add(TEntity user);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> f);
        void Remove(int id);
    }
}
