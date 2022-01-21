namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarRentingSystem.Services.Models.Cars;

    using static CarRentingSystem.Data.DataConstants.Car;

    public class CarFormModel
    {
        public CarFormModel()
        {
            this.Categories = new List<CarCategoryServiceModel>();
        }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string? Brand { get; init; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string? Model { get; init; }

        [Required]
        [MinLength(DescriptionMinLength)]
        public string? Description { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
    }
}
