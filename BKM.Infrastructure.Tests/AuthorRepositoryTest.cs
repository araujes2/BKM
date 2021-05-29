using BKM.Core.Entities;
using BKM.Core.Generic;
using BKM.Infrastructure.EntityFramework;
using EDAP.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace BKM.Infrastructure.Tests
{
    public class AuthorRepositoryTest
    {
        public AuthorRepositoryTest()
        {
            Seed();
        }

        private void Seed()
        {
            using (var context = new BKMContext(""))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Author()
                { 
                    ID = "Author1",
                    Name = "NameAuthor1",
                    DateOfBirth = DateTime.Now
                };

                one.Books.Add(new Book() 
                { 
                    Category = BookCategory.Category1,
                    ISBM = "Book1",
                    Title = "TitleB1",
                    LaunchDate = DateTime.Now
                });

                var two = new Author()
                {
                    ID = "Author2",
                    Name = "NameAuthor2",
                    DateOfBirth = DateTime.Now
                };

                var three = new Author()
                {
                    ID = "Author3",
                    Name = "NameAuthor3",
                    DateOfBirth = DateTime.Now
                };

                three.Books.Add(new Book()
                {
                    Category = BookCategory.Category3,
                    ISBM = "Book2",
                    Title = "TitleB3",
                    LaunchDate = DateTime.Now
                });

                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }

        [Test]
        public void Can_get_authors()
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                var books = repositoryProvider.Author.Load();
                Assert.IsTrue(books.Any());
            }
        }

        [Test]
        [TestCase("Author4", "01/20/2012", "AuthorName4")]
        public void Can_add_author(string ID, DateTime dateOfBirth, string name)
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                var author = repositoryProvider.Author.Add(new Author 
                { 
                    ID = ID,
                    DateOfBirth = dateOfBirth,
                    Name = name
                });
                repositoryProvider.UoW.SaveChanges();
                author = repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == ID);
                Assert.AreEqual(author.ID, ID);
            }
        }

        [Test]
        [TestCase("Author2")]
        public void Can_remove_author(string ID)
        {
            using (var repositoryProvider = new RepositoryProvider(""))
            {
                repositoryProvider.Author.Remove(ID);
                repositoryProvider.UoW.SaveChanges();
                var author = repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == ID);
                Assert.IsNull(author);
            }
        }
    }
}
