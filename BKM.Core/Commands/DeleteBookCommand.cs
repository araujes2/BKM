
using BKM.Core.Generic;
using BKM.Core.Responses;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class DeleteBookCommand : IRequest<DeleteBookResponse>
    {
        public string ISBM { get; set; }
    }
}
