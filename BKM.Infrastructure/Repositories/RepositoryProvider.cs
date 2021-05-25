


using BKM.Core.Interfaces;
using BKM.Infrastructure.EntityFramework;
using BKM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDAP.Infrastructure.Repositories
{
    public class RepositoryProvider : IRepositoryProvider, IDisposable
    {
        private readonly DbContext _dbContext;
        public RepositoryProvider(string connectionString)
        {
            _dbContext = new BKMContext(connectionString);
        }
        public IUnitOfWork UoW => new UnitOfWork(_dbContext);
        public IBookRepository Book => new BookRepository(_dbContext);
        public IAuthorRepository Author => new AuthorRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}