namespace ShoppingCart.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.ShoppingCart;
    using Services;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext db;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext db)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.userManager = userManager;
            this.db = db;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext
                .Session
                .GetShoppingCartId();

            var itemsWithDetails = this.GetCartItems(shoppingCartId);

            return this.View(itemsWithDetails);
        }

        public IActionResult Add(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartManager.Add(shoppingCartId, id);
            return this.RedirectToAction(nameof(this.Items));
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.GetCartItems(shoppingCartId);

            if (items.Count < 0)
            {
                return this.RedirectToAction(nameof(this.Items));
            }

            var order = new Order()
            {
                UserId = this.userManager.GetUserId(this.User)
            };

            foreach (var cartItem in items)
            {
                var orderItem = new OrderItem()
                {
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity
                };

                order.Items.Add(orderItem);
            }

            this.db.Add(order);
            this.db.SaveChanges();

            this.shoppingCartManager.Clear(shoppingCartId);
            return this.RedirectToAction(nameof(this.Items));
        }

        private List<CartItemViewModel> GetCartItems(string shoppingCartId)
        {
            var items = this.shoppingCartManager
                .GetItems(shoppingCartId);

            var itemsIds = items
                .Select(i => i.ProductId)
                .ToList();

            var itemsQuantities = items.ToDictionary(s => s.ProductId, s => s.Quantity);

            var itemsWithDetails = this.db.Products
                .Where(pr => itemsIds.Contains(pr.Id))
                .Select(pr => new CartItemViewModel
                {
                    ProductId = pr.Id,
                    Title = pr.Title,
                    Price = pr.Price,
                })
                .ToList();

            itemsWithDetails
                .ForEach(i => i.Quantity = itemsQuantities[i.ProductId]);

            return itemsWithDetails;
        }
    }
}
