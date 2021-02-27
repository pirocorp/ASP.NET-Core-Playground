namespace BookShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Services.Models.Author;
    using BookShop.Services.Models.Book;

    public interface IAuthorService
    {
        Task<int> CreateAsync(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<BooksByAuthorServiceModel>> BooksAsync(int authorId);

        Task<bool> ExistsAsync(int id);
    }
}
