namespace BookShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Author;

    public interface IAuthorService
    {
        Task<int> CreateAsync(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<BooksByAuthorServiceModel>> Books(int authorId);
    }
}
