using BKM.API;
using BKM.Core.Generic;
using MediatR;
using System;

namespace BKM.API
{
    public class CreateBookRequest : RequestBase, IRequest<CreateBookResponse>
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
    }
}
