
using System;

namespace BKM.Core.Interfaces
{
    public interface IRepositoryProvider : IDisposable
    {
        IUnitOfWork UoW { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
    }
}