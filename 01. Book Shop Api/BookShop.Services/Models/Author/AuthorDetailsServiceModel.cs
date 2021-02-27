namespace BookShop.Services.Models.Author
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class AuthorDetailsServiceModel : IMapFrom<Author>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Author, AuthorDetailsServiceModel>()
                .ForMember(d => d.Books, opt => opt.MapFrom(s => s.Books.Select(b => b.Title)));
        }
    }
}
