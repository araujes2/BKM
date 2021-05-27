
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class CreateAuthorCommand : CommandBase, IRequest<CreateAuthorResponse>
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public override bool IsValid()
        {
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            //if(rgx.IsMatch(model.ID) == false)
            //{
            //    throw new Exception("ISBM inválido");
            //}

            return string.IsNullOrEmpty(Name) == false & DateOfBirth > new DateTime(1990, 1, 1);
        }
    }
}
