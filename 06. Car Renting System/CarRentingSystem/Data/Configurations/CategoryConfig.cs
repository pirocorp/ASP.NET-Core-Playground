namespace CarRentingSystem.Data.Configurations
{
    using CarRentingSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Category;

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.HasKey(c => c.Id);

            category
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);
        }
    }
}
