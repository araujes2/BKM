
using BKM.Core.Commands;
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
    public class AuthorsControllerTest : ControllerTestBase
    {

        [SetUp]
        public void Setup()
        {
            ConfigureServer();
        }

        [Test]
        [Order(0)]
        public async Task Can_get_authors()
        {
            // arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "api/authors");

            // act
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //asset
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [Test]
        [Order(1)]
        [TestCase("00852760302", "AuthorNameTest", "09/25/1987")]
        public async Task Can_add_author(string ID, string name, DateTime dateOfBirth)
        {
            // arrange
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth              
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/authors");
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
        [TestCase("00852760302")]
        public async Task Can_delete_author(string ID)
        {
            // arrange
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/authors");
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