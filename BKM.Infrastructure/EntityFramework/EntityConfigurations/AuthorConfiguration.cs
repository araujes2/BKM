using BKM.Core.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BKM.Infrastructure.EntityFramework
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            //builder.ToTable("Author");
            builder.HasMany(e => e.Books)
             .WithOne(e => e.Author)
             .HasForeignKey(e => e.AuthorID);
        }
    }
}


