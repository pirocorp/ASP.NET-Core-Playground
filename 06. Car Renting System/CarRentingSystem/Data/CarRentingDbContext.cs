namespace CarRentingSystem.Data
{
    using CarRentingSystem.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarRentingDbContext : IdentityDbContext<User>
    {
        public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars => this.Set<Car>();

        public DbSet<Category> Categories => this.Set<Category>();

        public DbSet<Dealer> Dealers => this.Set<Dealer>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
