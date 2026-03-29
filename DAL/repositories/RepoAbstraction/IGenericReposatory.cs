using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repositories.RepoAbstraction
{
    public interface IGenericReposatory<TEntity,Tkey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity,bool>>? condition);
        Task<TEntity?> GetById(Tkey id);

        Task Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
