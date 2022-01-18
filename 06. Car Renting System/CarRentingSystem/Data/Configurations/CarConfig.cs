namespace CarRentingSystem.Data.Configurations
{
    using CarRentingSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static CarRentingSystem.Data.DataConstants.Car;

    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> car)
        {
            car.HasKey(c => c.Id);

            car
                .Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(CarBrandMaxLength);

            car
                .Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(CarModelMaxLength);

            car
                .Property(c => c.Description)
                .IsRequired();

            car
                .Property(c => c.ImageUrl)
                .IsRequired();

            car
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
