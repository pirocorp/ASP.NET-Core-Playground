namespace BookShop.Services.Models.Book
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BookShop.Common.Mapping;
    using BookShop.Data.Models;

    public class BooksByAuthorServiceModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public virtual void CreateMappings(IProfileExpression book)
        {
            book
                .CreateMap<Book, BooksByAuthorServiceModel>()
                .ForMember(b => b.Categories, opt => opt.MapFrom(b => b.Categories.Select(c => c.Category.Name)));
        }
    }
}
