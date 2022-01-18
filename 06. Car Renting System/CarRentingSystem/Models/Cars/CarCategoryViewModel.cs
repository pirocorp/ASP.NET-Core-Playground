namespace CarRentingSystem.Models.Cars
{
    using CarRentingSystem.Infrastructure.Exceptions;

    public class CarCategoryViewModel
    {
        private string? name;

        public int Id { get; init; }

        public string Name
        {
            get => this.name ?? throw new UninitializedPropertyException(nameof(this.Name));
            set => this.name = value;
        }
    }
}
