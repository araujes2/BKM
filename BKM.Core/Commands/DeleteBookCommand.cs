
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class DeleteBookCommand : CommandBase, IRequest<DeleteBookResponse>
    {
        public string ISBM { get; set; }
        public override bool IsValid()
        {
            return string.IsNullOrEmpty(ISBM) == false;
        }
    }
}
