namespace CarRentingSystem.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public Category(string name)
        {
            this.Name = name;

            this.Cars = new List<Car>();
        }

        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Car> Cars { get; init; }
    }
}
