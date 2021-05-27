
using Microsoft.EntityFrameworkCore;
using BKM.Core.Entities;
using BKM.Core.Interfaces;
using System.Linq;

namespace BKM.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DbContext context) : base(context)
        {
        }

     
    }
}
