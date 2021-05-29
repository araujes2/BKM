using BKM.Core.Commands;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BKM.API.Tests
{
    [TestFixture]
    public class CreateAuthorCommandValidationTest : TestBase
    {
        public CreateAuthorCommandValidationTest() : base()
        {

        }

        [Test]
        [TestCase("00852760302", "NameAuthorValidation", "09/25/1987")]
        public void Is_valid_author(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };
            var validation = new CreateAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsTrue(validation.IsValid());
        }

        [Test]
        [TestCase("", "NameAuthorValidation", "09/25/1987")]
        public void Is_not_valid_when_id_is_null_or_empty(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };
            var validation = new CreateAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());
        }

        [Test]
        [TestCase("00852760302", "", "09/25/1987")]
        public void Is_not_valid_when_name_is_null_or_empty(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };
            var validation = new CreateAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());
        }

        [Test]
        [TestCase("00852760302", "NameAuthorValidation", "09/25/2021")]
        public void Is_not_valid_when_date_of_birth_is_lower_30(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };
            var validation = new CreateAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());
        }

        [Test]
        [TestCase("Author1", "NameAuthor1", "09/25/1987")]
        public void Is_not_valid_when_author_exists(string ID, string name, DateTime dateOfBirth)
        {
            var command = new CreateAuthorCommand()
            {
                ID = ID,
                Name = name,
                DateOfBirth = dateOfBirth,
                Today = DateTime.Today
            };
            var validation = new CreateAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }

    }
}