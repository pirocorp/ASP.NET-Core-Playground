namespace CarRentingSystem.Data.Configurations
{
    using CarRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Dealer;

    public class DealerConfig : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> dealer)
        {
            dealer.HasKey(d => d.Id);

            dealer
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            dealer
                .Property(d => d.PhoneNumber)
                .IsRequired()
                .HasMaxLength(PhoneNumberMaxLength);

            dealer
                .Property(d => d.UserId)
                .IsRequired();

            // One to one relation and dependent entity is dealer.
            dealer
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
