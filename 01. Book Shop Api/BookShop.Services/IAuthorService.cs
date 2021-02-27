namespace BookShop.Services
{
    using System.Threading.Tasks;
    using Models.Author;

    public interface IAuthorService
    {
        Task<AuthorDetailsServiceModel> DetailsAsync(int id);

        Task<int> CreateAsync(string firstName, string lastName);
    }
}
