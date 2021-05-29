
using BKM.Core.Generic;
using BKM.Core.Responses;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class CreateAuthorCommand : IRequest<CreateAuthorResponse>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? Today { get; set; }
    }
}
