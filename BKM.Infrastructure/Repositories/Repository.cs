
using BKM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKM.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        public DbContext Context
        {
            get
            {
                return _context;
            }
        }
        public Repository(DbContext context)
        {
            _context = context;
        }
        public virtual TEntity Add(TEntity model)
        {
            return _context.Set<TEntity>().Add(model).Entity;
        }
        public virtual IQueryable<TEntity> Load()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
        public virtual void Edit(TEntity model)
        {
            _context.Set<TEntity>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }
        public virtual void Remove(params object[] keyValues)
        {
            var model = _context.Set<TEntity>().Find(keyValues);
            if(model != null)
            {
                if (_context.Entry(model).State == EntityState.Detached)
                {
                    _context.Set<TEntity>().Attach(model);
                }
                _context.Set<TEntity>().Remove(model);
            }
        }
    }
}




