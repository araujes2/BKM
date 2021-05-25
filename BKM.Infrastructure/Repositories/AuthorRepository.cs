
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

        public override Author Add(Author model)
        {
            if (IsValid(model))
            {
                model = base.Add(model);
            }
            return model;
        }

        private bool IsValid(Author model)
        {
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            //if(rgx.IsMatch(model.ID) == false)
            //{
            //    throw new Exception("CPF inválido");
            //}

            return true;

        }
    }
}
