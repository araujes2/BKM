using BKM.Core.Entities;
using BKM.Core.Generic;
using EDAP.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace BKM.Infrastructure.Tests
{
    [TestFixture]
    public class BookRepositoryTest
    {
        public BookRepositoryTest()
        {
        }

        [Test]
        public void Can_get_books()
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                var books = repositoryProvider.Book.Load();
                Assert.IsTrue(books.Any());
            }
        }

        [Test]
        [TestCase("Book2", "TitleB2", BookCategory.Category1, "01/20/2012", "Author1")]
        public void Can_add_book(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                var book = repositoryProvider.Book.Add(new Book 
                { 
                    ISBM = ISBM,
                    AuthorID = authorID,
                    Category = category,
                    LaunchDate = launchDate,
                    Title = title
                });
                repositoryProvider.UoW.SaveChanges();
                book = repositoryProvider.Book.Load().FirstOrDefault(m => m.ISBM == ISBM);
                Assert.AreEqual(book.ISBM, ISBM);
            }
        }
        [TestCase("Book1")]
        public void Can_remove_book(string ISBM)
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                repositoryProvider.Book.Remove("1");
                repositoryProvider.UoW.SaveChanges();
                var book = repositoryProvider.Book.Load().FirstOrDefault(m => m.ISBM == ISBM);
                Assert.IsNull(book);
            }
        }

    }
}
