using BKM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BKM.Infrastructure.Tests
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public int SaveChanges()
        {
            return 1;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await Task.FromResult(1);
        }
    }
}