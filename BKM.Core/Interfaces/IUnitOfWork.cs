using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKM.Core.Interfaces
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
