
using Microsoft.EntityFrameworkCore;
using BKM.Core.Entities;
using BKM.Core.Interfaces;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace BKM.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbContext context) : base(context)
        {
        }




    }
}
