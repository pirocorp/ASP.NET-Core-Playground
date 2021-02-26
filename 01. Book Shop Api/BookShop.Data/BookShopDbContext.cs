namespace BookShop.Data
{
    using Models;

    public class BookShopDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookShopDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<BookShopDbContext> options)
            : base(options)
        { }

        public Microsoft.EntityFrameworkCore.DbSet<Book> Books { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Author> Authors { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<BookCategory> BooksCategories { get; set; }

        /// <summary>
        /// Configures the DbContext
        /// </summary>
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            global::BookShop.Data.BookShopDbContext.ConfigureRelations(builder);
        }

        /// <summary>
        /// Applies all entity configurations which implements IEntityTypeConfiguration.
        /// </summary>
        private static void ConfigureRelations(Microsoft.EntityFrameworkCore.ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(Book).Assembly);
    }
}
