
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class DeleteAuthorCommand : CommandBase, IRequest<DeleteAuthorResponse>
    {
        public string ID { get; set; }
        public override bool IsValid()
        {
            return string.IsNullOrEmpty(ID) == false;
        }
    }
}
