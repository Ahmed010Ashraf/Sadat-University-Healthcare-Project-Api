using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repositories.RepoAbstraction
{
    public interface IUOW
    {

        IGenericReposatory<TEntity, Tkey> GetReposatory<TEntity, Tkey>()where TEntity : class;
        Task<int> SaveChnagesAsync();
    }
}
