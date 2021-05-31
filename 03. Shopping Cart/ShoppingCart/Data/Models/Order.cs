namespace ShoppingCart.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class Order
    {
        public Order()
        {
            this.Items = new List<OrderItem>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
