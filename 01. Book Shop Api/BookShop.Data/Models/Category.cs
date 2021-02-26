namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [global::System.ComponentModel.DataAnnotations.StringLength(CategoryNamMaxLength)]
        public string Name { get; set; }

        public global::System.Collections.Generic.IEnumerable<BookCategory> Books { get; set; }
    }
}
