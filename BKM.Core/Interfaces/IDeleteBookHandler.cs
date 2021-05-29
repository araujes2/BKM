using BKM.Core.Commands;
using BKM.Core.Responses;
using MediatR;


namespace BKM.Core.Interfaces
{
    public interface IDeleteBookHandler : IRequestHandler<DeleteBookCommand, DeleteBookResponse>
    {
       
    }
}
