namespace ShoppingCart.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly List<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items 
            => new List<CartItem>(this.items);
        
        public void Add(int productId)
        {
            var cartItem = this.Items.FirstOrDefault(i => i.ProductId.Equals(productId));

            if (cartItem is null)
            {
                cartItem = new CartItem()
                {
                    ProductId = productId
                };

                this.items.Add(cartItem);
            }

            cartItem.Quantity += 1;
        }

        public void Remove(CartItem cartItem)
        {
            // Null check ?!
            this.items.Remove(cartItem);
        }

        public void Update(CartItem cartItem)
        {
            var currentItem = this.items.Find(i => i.ProductId.Equals(cartItem.ProductId));

            if (currentItem is null)
            {
                return;
            }

            currentItem.Quantity = currentItem.Quantity;
        }

        public void Clear() => this.items.Clear();
    }
}
