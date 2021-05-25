
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

        public override Book Add(Book model)
        {
            if(IsValid(model))
            {
                model = base.Add(model);
            }
            return model;
        }

        private bool IsValid(Book model)
        {
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            //if(rgx.IsMatch(model.ISBM) == false)
            //{
            //    throw new Exception("ISBM inválido");
            //}

            if(_context.Set<Author>().FirstOrDefault(m => m.ID == model.AuthorID) == null)
            {
                throw new Exception("Auhtor não localizado");
            }

            return true;

        }


    }
}
