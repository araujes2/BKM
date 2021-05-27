
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class CreateBookCommand : CommandBase, IRequest<CreateBookResponse>
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
        public override bool IsValid()
        {
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            //if(rgx.IsMatch(model.ISBM) == false)
            //{
            //    throw new Exception("ISBM inválido");
            //}

            return string.IsNullOrEmpty(ISBM) || string.IsNullOrEmpty(Title) ? false : true;
        }
    }
}
