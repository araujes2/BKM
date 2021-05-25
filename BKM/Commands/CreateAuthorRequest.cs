using BKM.API;
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.API
{
    public class CreateAuthorRequest : IRequest<CreateAuthorResponse>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
