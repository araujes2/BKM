
using BKM.Core.Commands;
using BKM.Core.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BKM.API.Tests
{
    [TestFixture]
    public class BooksControllerTest : ControllerTestBase
    {

        [SetUp]
        public void Setup()
        {
            ConfigureServer();
        }

        [Test]
        [Order(0)]
        public async Task Can_get_books()
        {
            // arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "api/books");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //asset
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        [Order(1)]
        [TestCase("Book3", "TitleB3", BookCategory.Category1, "01/20/2012", "00346255341")]
        public async Task Can_add_book(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new CreateBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/books");
            var contentString = JsonConvert.SerializeObject(command);
            request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //asset
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        }

        [Test]
        [Order(2)]
        [TestCase("Book3")]
        public async Task Can_delete_book(string ISBM)
        {
            // arrange
            var command = new DeleteBookCommand()
            {
                ISBM = ISBM
            };

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/books");
            var contentString = JsonConvert.SerializeObject(command);
            request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //asset
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

    }
}