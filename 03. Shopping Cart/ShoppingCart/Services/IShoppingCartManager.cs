namespace ShoppingCart.Services
{
    using System.Collections.Generic;
    using Models;

    public interface IShoppingCartManager
    {
        void Add(string id, int productId);

        void Update(string id, CartItem cartItem);

        void Remove(string id, CartItem cartItem);

        void Clear(string id);

        IEnumerable<CartItem> GetItems(string id);
    }
}
