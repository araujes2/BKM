using BKM.Core.Commands;
using BKM.Core.Generic;
using NUnit.Framework;
using System;

namespace BKM.API.Tests
{
    [TestFixture]
    public class AlterBookCommandValidationTest : TestBase
    {
        public AlterBookCommandValidationTest() : base()
        {

        }

        [Test]
        [TestCase("Book1", "TitleB3", BookCategory.Category1, "01/20/2012", "00852760302")]
        public void Is_valid_book_edit(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new AlterBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };
            var validation = new AlterBookCommandValidation(_repositoryProvider, command);
            Assert.IsTrue(validation.IsValid());
        }

        [Test]
        [TestCase("Book1", "TitleB4", BookCategory.Category1, "01/20/2012", "Author4")]
        public void Is_not_valid_when_author_does_not_exist(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new AlterBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var validation = new AlterBookCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }

        [Test]
        [TestCase("", "TitleB3", BookCategory.Category1, "01/20/2012", "00852760302")]
        public void Is_not_valid_when_ISBM_is_null_or_empty(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new AlterBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var validation = new AlterBookCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }

        [Test]
        [TestCase("Book1", "", BookCategory.Category1, "01/20/2012", "00852760302")]
        public void Is_not_valid_when_title_is_null_or_empty(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new AlterBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var validation = new AlterBookCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }

        [Test]
        [TestCase("Book5", "", BookCategory.Category1, "01/20/2012", "00852760302")]
        public void Is_not_valid_when_book_does_not_exist(string ISBM, string title, BookCategory category, DateTime launchDate, string authorID)
        {
            var command = new CreateBookCommand()
            {
                ISBM = ISBM,
                Category = category,
                LaunchDate = launchDate,
                AuthorID = authorID,
                Title = title
            };

            var validation = new CreateBookCommandValidation(_repositoryProvider, command);
            Assert.IsFalse(validation.IsValid());

        }



    }
}