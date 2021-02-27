namespace BookShop.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookShop.Common.Mapping;
    using BookShop.Data;
    using BookShop.Services.Models.Book;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly BookShopDbContext dbContext;
        
        public BookService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Create(string title, string description, decimal price, int copies, int? edition, int? ageRestriction,
            DateTime releaseDate, int authorId, string categories)
        {
            var categoryNames = categories
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.dbContext.Categories
                .Where(c => categoryNames.Contains(c.Name))
                .ToListAsync();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.Any(c => c.Name.Equals(categoryName)))
                {
                    continue;
                }

                var category = new Category()
                {
                    Name = categoryName
                };

                await this.dbContext.AddAsync(category);
                allCategories.Add(category);
            }

            await this.dbContext.SaveChangesAsync();

            var book = new Book()
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId,
            };

            allCategories.ForEach(category => book.Categories.Add(new BookCategory
            {
                Category = category,
                Book = book
            }));

            await this.dbContext.AddAsync(book);
            await this.dbContext.SaveChangesAsync();

            return book.Id;
        }

        public async Task<IEnumerable<BookListingServiceModel>> AllAsync(string searchWord)
            => await this.dbContext.Books
                .Where(b => b.Title.ToLower().Contains(searchWord.ToLower()) || b.Description.ToLower().Contains(searchWord.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .To<BookListingServiceModel>()
                .ToListAsync();

        public async Task<BookDetailsServiceModel> DetailsAsync(int id)
            => await this.dbContext.Books
                .Where(b => b.Id.Equals(id))
                .To<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();
    }
}
