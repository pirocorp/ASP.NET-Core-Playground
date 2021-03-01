namespace BookShop.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Services.Models.Book;

    public interface IBookService
    {
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<BookListingServiceModel>> AllAsync(string searchWord);

        Task<BookDetailsServiceModel> DetailsAsync(int id);

        Task<int> CreateAsync(
            string title, 
            string description, 
            decimal price, 
            int copies, 
            int? edition, 
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);

        Task<int> UpdateAsync(
            int id,
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId);

        Task DeleteAsync(int id);
    }
}
