using BKM.Core.Commands;
using BKM.Core.Responses;
using MediatR;
using System.Threading.Tasks;

namespace BKM.Core.Interfaces
{
    public interface ICreateBookHandler : IRequestHandler<CreateBookCommand, CreateOrAlterBookResponse>
    {
    }
}
