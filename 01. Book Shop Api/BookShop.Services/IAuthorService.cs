namespace BookShop.Services
{
    using Models.Author;

    public interface IAuthorService
    {
        AuthorDetailsServiceModel Details(int id);
    }
}
