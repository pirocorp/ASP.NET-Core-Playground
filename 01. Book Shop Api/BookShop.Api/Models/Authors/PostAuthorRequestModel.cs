namespace BookShop.Api.Models.Authors
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class PostAuthorRequestModel
    {
        [Required]
        [StringLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(AuthorNameMaxLength)]
        public string LastName { get; set; }
    }
}
