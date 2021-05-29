using BKM.Core.Interfaces;
using System;

namespace BKM.Infrastructure.Tests
{
    public class FakeRepositoryProvider : IRepositoryProvider, IDisposable
    {
        public IUnitOfWork UoW => new FakeUnitOfWork();

        public IBookRepository Book => throw new NotImplementedException();

        public IAuthorRepository Author => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}
