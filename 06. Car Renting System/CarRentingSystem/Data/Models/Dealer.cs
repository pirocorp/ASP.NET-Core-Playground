namespace CarRentingSystem.Data.Models
{
    using System.Collections.Generic;

    public class Dealer
    {
        public Dealer(string name, string phoneNumber, string userId)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.UserId = userId;

            this.Cars = new List<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; set; }
    }
}
