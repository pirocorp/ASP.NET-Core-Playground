namespace ShoppingCart.Services
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using Models;

    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void Add(string id, int productId)
        {
            var shoppingCart = this.GetCurrentShoppingCart(id);
            shoppingCart.Add(productId);
        }

        public void Update(string id, CartItem cartItem)
        {
            var shoppingCart = this.GetCurrentShoppingCart(id);
            shoppingCart.Update(cartItem);
        }

        public void Remove(string id, CartItem cartItem)
        {
            var shoppingCart = this.GetCurrentShoppingCart(id);
            shoppingCart.Remove(cartItem);
        }

        public void Clear(string id)
            => this.GetCurrentShoppingCart(id).Clear();

        public IEnumerable<CartItem> GetItems(string id)
            => new List<CartItem>(this.GetCurrentShoppingCart(id).Items);

        private ShoppingCart GetCurrentShoppingCart(string id)
            => this.carts.GetOrAdd(id, new ShoppingCart());
    }
}
