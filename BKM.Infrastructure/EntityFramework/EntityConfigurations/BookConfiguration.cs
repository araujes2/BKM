using BKM.Core.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BKM.Infrastructure.EntityFramework
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.ToTable("Book");
            builder.HasKey(e => e.ISBM);
        }
    }
}


