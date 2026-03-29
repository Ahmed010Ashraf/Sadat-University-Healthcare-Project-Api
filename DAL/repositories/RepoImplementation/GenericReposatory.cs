using DAL.Context;
using DAL.repositories.RepoAbstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.repositories.RepoImplementation
{
    public class GenericReposatory<TEntity, Tkey>(AppDbContext _context) : IGenericReposatory<TEntity, Tkey> where TEntity : class
    {

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? condition)
        {
            if(condition == null)
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            return await _context.Set<TEntity>().Where(condition).ToListAsync();
        }

        public async Task<TEntity?> GetById(Tkey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

     

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

      

      
    }
}
