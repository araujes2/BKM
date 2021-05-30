


using BKM.Core.Interfaces;
using BKM.Infrastructure.EntityFramework;
using BKM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDAP.Infrastructure.Repositories
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly DbContext _dbContext;
        public RepositoryProvider(DbContextOptions options)
        {
            _dbContext = new BKMContext(options);
        }
        public IUnitOfWork UoW => new UnitOfWork(_dbContext);
        public IBookRepository Book => new BookRepository(_dbContext);
        public IAuthorRepository Author => new AuthorRepository(_dbContext);
        public IUserRepository User => new UserRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}