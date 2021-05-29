using BKM.Core.Commands;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.API.Tests
{
    [TestFixture]
    public class DeleteBookHandlerTest : HandlerTestBase
    {
        protected DeleteBookHandler _handler;
        public DeleteBookHandlerTest() : base()
        {
            _handler = new DeleteBookHandler(_repositoryProvider, new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        [TestCase("Book1")]
        public async Task Can_delete_book(string ISBM)
        {
            var command = new DeleteBookCommand()
            {
                ISBM = ISBM
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(200, response.Status);
        }

        [Test]
        [TestCase("")]
        public async Task Cannot_delete_book_with_invalid_input(string ISBM)
        {
            var command = new DeleteBookCommand()
            {
                ISBM = ISBM
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }

    }
}