using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace src.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ILogger<Repository<TEntity>> _logger;
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;


        public Repository(ILogger<Repository<TEntity>> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> functionToInclude)
        {
            return await _dbSet.Include(functionToInclude).ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetSingleAsync<TProperty>(Expression<Func<TEntity, bool>> searchCondition, Expression<Func<TEntity, TProperty>> functionToInclude)
        {
            return await _dbSet.Include(functionToInclude).FirstAsync(searchCondition);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
