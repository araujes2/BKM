
using BKM.Core.Generic;
using BKM.Core.Responses;
using MediatR;
using System;

namespace BKM.Core.Commands
{
    public class CreateBookCommand : IRequest<CreateBookResponse>
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
    }
}
