
namespace BKM.Core.Interfaces
{
    public interface IRepositoryProvider
    {
        IUnitOfWork UoW { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
    }
}