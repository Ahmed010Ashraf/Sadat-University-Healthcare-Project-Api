using DAL.Context;
using DAL.repositories.RepoAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repositories.RepoImplementation
{
    public class UOW(AppDbContext _context) : IUOW
    {
        private Dictionary<string, object> Container = [];
        public IGenericReposatory<TEntity, Tkey> GetReposatory<TEntity, Tkey>() where TEntity : class
        {
            var RepoName = typeof(TEntity).Name;
            if (Container.ContainsKey(RepoName)) { 
                return (IGenericReposatory<TEntity, Tkey>) Container[RepoName];
            }

            
            Container.Add(RepoName,new GenericReposatory<TEntity,Tkey>(_context));
            return (IGenericReposatory<TEntity, Tkey>)Container[RepoName];
        }

        public async Task<int> SaveChnagesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
