namespace BookShop.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Services.Models.Book;

    public interface IBookService
    {
        Task<IEnumerable<BookListingServiceModel>> AllAsync(string searchWord);

        Task<BookDetailsServiceModel> DetailsAsync(int id);

        Task<int> Create(
            string title, 
            string description, 
            decimal price, 
            int copies, 
            int? edition, 
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);
    }
}
