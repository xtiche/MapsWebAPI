using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Abstract.Repositories
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        IList<TEntity> GetAll();
        TEntity GetById(TKey id);
        TKey Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TKey id);
    }
}
