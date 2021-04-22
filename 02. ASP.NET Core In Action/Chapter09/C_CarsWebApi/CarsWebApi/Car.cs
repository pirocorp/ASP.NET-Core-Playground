namespace CarsWebApi
{
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
