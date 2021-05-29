
using BKM.Core.Commands;
using BKM.Core.Responses;
using MediatR;

namespace BKM.Core.Interfaces
{
    public interface IDeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, DeleteAuthorResponse>
    {
       
    }
}
