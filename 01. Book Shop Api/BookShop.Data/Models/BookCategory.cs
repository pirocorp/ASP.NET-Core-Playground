namespace BookShop.Data.Models
{
    public class BookCategory : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<BookCategory>
    {
        public int BookId { get; set; }

        public Book Book { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BookCategory> bookCategory)
        {
            bookCategory
                .HasKey(k => new { k.BookId, k.CategoryId });


            bookCategory
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(bc => bc.BookId);


            bookCategory
                .HasOne(bc => bc.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
