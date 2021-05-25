using System.Linq;

namespace BKM.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity model);
        void Edit(TEntity model);
        void Remove(params object[] keyValues);
        IQueryable<TEntity> Load();
    }
}
