using BKM.Core.Commands;
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
    public class CreateAuthorHandlerTest : TestBase
    {
        protected CreateAuthorHandler _handler;
        public CreateAuthorHandlerTest() : base()
        {
            _handler = new CreateAuthorHandler(_repositoryProvider, _mapper, new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        [TestCase("00852760302", "NameAuthor4", "09/25/1987")]
        public async Task Can_add_author_HTTP201(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(201, response.Status);
        }

        [Test]
        [TestCase("00852760302", "NameAuthor1", "09/25/1987")]
        public async Task Cannot_add_existing_author_HTTP400(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };

            var response = await _handler.Handle(command, CancellationToken.None);
            Assert.AreEqual(400, response.Status);

        }
    }
}