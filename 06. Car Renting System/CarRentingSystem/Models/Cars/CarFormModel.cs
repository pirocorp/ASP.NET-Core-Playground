namespace CarRentingSystem.Models.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CarRentingSystem.Infrastructure.Exceptions;
    using CarRentingSystem.Services.Models.Cars;

    using static CarRentingSystem.Data.DataConstants.Car;

    public class CarFormModel : ICarModel
    {
        public CarFormModel()
        {
            this.Brand = string.Empty;
            this.Model = string.Empty;

            this.Categories = new List<CarCategoryServiceModel>();
        }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }

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
