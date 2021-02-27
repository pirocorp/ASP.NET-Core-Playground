namespace BookShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static DataConstants;

    public class Book : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Book>
    {
        public Book()
        {
            this.Categories = new List<BookCategory>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public List<BookCategory> Categories { get; set; }

        public void Configure(EntityTypeBuilder<Book> book)
        {
            book
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
