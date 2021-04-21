using BookShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Infrastructure.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            //builder.Property(u => u.Id).ValueGeneratedOnAdd();

            //builder.HasMany(u => u.Author).WithOne(u => u.ActivityType).HasForeignKey(u => u.ActivityTypeId);

            builder.HasKey(sc => new { sc.AuthorId, sc.BookId });

            builder.HasOne(sc => sc.Book)
                .WithMany(s => s.BookAuthors)
                .HasForeignKey(sc => sc.BookId);


            builder.HasOne(sc => sc.Author)
                .WithMany(s => s.BookAuthors)
                .HasForeignKey(sc => sc.AuthorId);
        }
    }
}
