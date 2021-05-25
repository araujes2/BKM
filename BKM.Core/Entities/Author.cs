using System;
using System.Collections.Generic;
using System.Text;

namespace BKM.Core.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
