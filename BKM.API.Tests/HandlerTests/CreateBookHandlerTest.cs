using BKM.Core.Commands;
using BKM.Core.Generic;
using BKM.Core.Handlers;
using BKM.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.API.Tests
{
    [TestFixture]
    public class CreateBookHandlerTest : TestBase
    {
        protected ICreateBookHandler _handler;
        public CreateBookHandlerTest() : base()
        {
            _handler = new CreateBookHandler(_repositoryProvider, _mapper, new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        [TestCase("Book3", "TitleB3", BookCategory.Category1, "01/20/2012", "00852760302")]
        public async Task Can_add_book_HTTP201(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new CreateBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };
            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(201, response.Status);
        }

        [Test]
        [TestCase("Book1", "TitleB1", BookCategory.Category1, "01/20/2012", "00852760302")]
        public async Task Cannot_add_existing_book_HTTP400(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new CreateBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }

        [Test]
        [TestCase("Book4", "TitleB4", BookCategory.Category1, "01/20/2012", "Author4")]
        public async Task Cannot_add_book_with_invalid_author_HTTP404(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new CreateBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }

    }
}