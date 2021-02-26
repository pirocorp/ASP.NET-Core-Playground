namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Book : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Book>
    {
        public Book()
        {
            this.Categories = new System.Collections.Generic.HashSet<BookCategory>();
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

        public System.DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public System.Collections.Generic.IEnumerable<BookCategory> Categories { get; set; }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> book)
        {
            book
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
