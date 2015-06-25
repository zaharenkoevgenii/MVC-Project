using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(int n = 0);
        void Add(TEntity user);
        void Remove(int id);
    }
}
