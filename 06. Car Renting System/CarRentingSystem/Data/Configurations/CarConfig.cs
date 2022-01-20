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
                .HasMaxLength(BrandMaxLength);

            car
                .Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(ModelMaxLength);

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

            car
                .HasOne(c => c.Dealer)
                .WithMany(d => d.Cars)
                .HasForeignKey(c => c.DealerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
