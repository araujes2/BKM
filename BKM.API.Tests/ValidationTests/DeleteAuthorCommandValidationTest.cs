using BKM.Core.Commands;
using NUnit.Framework;

namespace BKM.API.Tests
{
    [TestFixture]
    public class DeleteAuthorCommandValidationTest : TestBase
    {
        public DeleteAuthorCommandValidationTest() : base()
        {

        }

        [Test]
        [TestCase("00346255341")]
        public void Is_valid(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };
            var validation = new DeleteAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsTrue(validation.IsValid());
        }

        [Test]
        [TestCase("Author5")]
        public void Is_not_valid_when_auhtor_does_not_exists(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var validation = new DeleteAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }

        [Test]
        [TestCase("00852760302")]
        public void Is_not_valid_when_author_has_books(string ID)
        {
            var command = new DeleteAuthorCommand()
            {
                ID = ID
            };

            var validation = new DeleteAuthorCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }



    }
}