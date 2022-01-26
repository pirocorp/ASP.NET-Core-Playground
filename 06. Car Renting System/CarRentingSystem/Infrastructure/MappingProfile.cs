namespace CarRentingSystem.Infrastructure
{
    using AutoMapper;

    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Services.Models.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CarCategoryServiceModel>();

            this.CreateMap<Car, CarLatestServiceModel>();
            this.CreateMap<CarDetailsServiceModel, CarFormModel>();

            this
                .CreateMap<Car, CarServiceModel>()
                .ConstructUsing(c => new CarServiceModel(
                    c.Id,
                    c.Brand,
                    c.Category.Name,
                    c.Model,
                    c.ImageUrl,
                    c.IsPublic,
                    c.Year));

            this
                .CreateMap<Car, CarDetailsServiceModel>()
                .ConstructUsing(c => new CarDetailsServiceModel(
                    c.Id,
                    c.Brand,
                    c.Category.Name,
                    c.Model,
                    c.ImageUrl,
                    c.IsPublic,
                    c.Year,
                    c.CategoryId,
                    c.DealerId,
                    c.Dealer.Name,
                    c.Description,
                    c.Dealer.UserId));
        }
    }
}
