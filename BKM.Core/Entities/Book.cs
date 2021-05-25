using BKM.Core.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BKM.Core.Entities
{
    public class Book
    {
        public string ISBM { get; set; }
        public string Title { get; set; }
        public BookCategory Category { get; set; }
        public DateTime LaunchDate { get; set; }
        public string AuthorID { get; set; }
        public Author Author { get; set; }
    }
}
