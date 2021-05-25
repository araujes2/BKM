
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Generic
{
    public class CreateAuthorCommandRequest : CommandRequestBase, IRequest<CreateAuthorResponse>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public override bool IsValid()
        {
            return string.IsNullOrEmpty(ID) == false & string.IsNullOrEmpty(Name) == false & DateOfBirth > new DateTime(1990, 1, 1);
        }
    }
}
