namespace CarRentingSystem.Infrastructure
{
    using AutoMapper;

    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Models.Cars;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Car, CarLatestServiceModel>();
            this.CreateMap<CarDetailsServiceModel, CarFormModel>();

            this
                .CreateMap<Car, CarDetailsServiceModel>()
                .ConstructUsing(c => new CarDetailsServiceModel(
                    c.Id,
                    c.Brand,
                    c.CategoryId,
                    c.Category.Name,
                    c.Model,
                    c.ImageUrl,
                    c.Year,
                    c.DealerId,
                    c.Dealer.Name,
                    c.Description,
                    c.Dealer.UserId));
        }
    }
}
