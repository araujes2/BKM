using BKM.Core.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BKM.Core.DTO
{
    public class DtoBook
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
    }
}
