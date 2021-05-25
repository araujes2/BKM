
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.Core.Generic
{
    public class CreateBookCommandRequest : CommandRequestBase, IRequest<CreateBookResponse>
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
        public override bool IsValid()
        {
            return string.IsNullOrEmpty(ISBM) == false & string.IsNullOrEmpty(Title) == false & string.IsNullOrEmpty(AuthorID) == false;
        }
    }
}
