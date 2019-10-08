using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicWorm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Models
{
    public class ShoppingCart
    {
        private readonly WormDbContext _wormDbContext;

        public ShoppingCart(WormDbContext wormDbContext)
        {
            _wormDbContext = wormDbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<WormDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem =
                _wormDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _wormDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _wormDbContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _wormDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _wormDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _wormDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _wormDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product)
                .ThenInclude(a => a.Artist)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _wormDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _wormDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _wormDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _wormDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
