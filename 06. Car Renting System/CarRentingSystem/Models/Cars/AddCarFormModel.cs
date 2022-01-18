namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static CarRentingSystem.Data.DataConstants.Car;

    public class AddCarFormModel
    {
        public AddCarFormModel()
        {
            this.Categories = new List<CarCategoryViewModel>();
        }

        [Required]
        [StringLength(CarBrandMaxLength, MinimumLength = CarBrandMinLength)]
        public string? Brand { get; init; }

        [Required]
        [StringLength(CarModelMaxLength, MinimumLength = CarModelMinLength)]
        public string? Model { get; init; }

        [Required]
        [MinLength(CarDescriptionMinLength)]
        public string? Description { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; init; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}
