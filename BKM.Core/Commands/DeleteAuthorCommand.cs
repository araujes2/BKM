
using BKM.Core.Generic;
using BKM.Core.Responses;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class DeleteAuthorCommand : IRequest<DeleteAuthorResponse>
    {
        public string ID { get; set; }
    }
}
