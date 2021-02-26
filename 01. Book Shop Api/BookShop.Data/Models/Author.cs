namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Author
    {
        public Author()
        {
            this.Books = new System.Collections.Generic.HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(AuthorNameMaxLength)]
        public string LastName { get; set; }

        public System.Collections.Generic.IEnumerable<Book> Books { get; set; }   
    }
}
