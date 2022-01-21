namespace CarRentingSystem.Data.Configurations
{
    using CarRentingSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.User;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.FullName)
                .HasMaxLength(FullNameMaxLength);
        }
    }
}
