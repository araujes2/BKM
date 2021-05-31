using AutoMapper;
using BKM.API.Utilities;
using BKM.Core.Entities;
using BKM.Core.Generic;
using BKM.Core.Interfaces;
using BKM.Infrastructure.EntityFramework;
using EDAP.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace BKM.API.Tests
{
    public abstract class TestBase : IDisposable
    {
        protected IMapper _mapper;
        protected IRepositoryProvider _repositoryProvider;

        public TestBase()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SourceMappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();

            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var dbOptions = new DbContextOptionsBuilder<BKMContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new BKMContext(dbOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var a1 = new Author()
                {
                    ID = "00852760302",
                    Name = "NameAuthor1",
                    DateOfBirth = DateTime.Now
                };

                var a2 = new Author()
                {
                    ID = "00346255341",
                    Name = "NameAuthor2",
                    DateOfBirth = DateTime.Now
                };

                var a3 = new Author()
                {
                    ID = "Author3",
                    Name = "NameAuthor3",
                    DateOfBirth = DateTime.Now
                };

                var b1 = new Book()
                {
                    AuthorID = "00852760302",
                    Category = BookCategory.Category1,
                    ISBM = "Book1",
                    Title = "TitleB1",
                    LaunchDate = DateTime.Now
                };

                var b2 = new Book()
                {
                    AuthorID = "Author3",
                    Category = BookCategory.Category3,
                    ISBM = "Book2",
                    Title = "TitleB2",
                    LaunchDate = DateTime.Now
                };

                context.AddRange(a1, a2, a3, b1, b2);

                context.SaveChanges();
            }

            _repositoryProvider = new RepositoryProvider(dbOptions);

        }
        public void Dispose()
        {
            _repositoryProvider.Dispose();
        }
    }
}