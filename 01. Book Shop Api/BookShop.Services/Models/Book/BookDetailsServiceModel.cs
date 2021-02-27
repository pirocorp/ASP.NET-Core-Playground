namespace BookShop.Services.Models.Book
{
    using System.Linq;
    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;

    public class BookDetailsServiceModel : BooksByAuthorServiceModel, IMapFrom<Book>, IHaveCustomMappings
    {
        public string Author { get; set; }

        public override void CreateMappings(IProfileExpression book)
        {
            book
                .CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(d => d.Categories, opt => opt.MapFrom(s => s.Categories.Select(c => c.Category.Name)))
                .ForMember(d => d.Author, opt => opt.MapFrom(s => $"{s.Author.FirstName} {s.Author.LastName}"));
        }
    }
}
