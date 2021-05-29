using BKM.Core.Commands;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.API.Tests
{
    [TestFixture]
    public class DeleteAuthorHandlerTest : HandlerTestBase
    {
        protected DeleteAuthorHandler _handler;
        public DeleteAuthorHandlerTest() : base()
        {
            _handler = new DeleteAuthorHandler(_repositoryProvider, new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        [TestCase("Author2")]
        public async Task Can_delete_author(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(200, response.Status);
        }

        [Test]
        [TestCase("Author1")]
        public async Task Cannot_delete_author_with_books(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }

        [Test]
        [TestCase("")]
        public async Task Cannot_delete_author_with_invalid_input(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }

    }
}