using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MusicWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WormDbContext _wormDbContext;
        private readonly ShoppingCart _shoppingCart;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public OrderRepository(WormDbContext wormDbContext, 
            ShoppingCart shoppingCart, 
            IHttpContextAccessor httpContextAccessor)
        {
            _wormDbContext = wormDbContext;
            _shoppingCart = shoppingCart;
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateOrder(Order order, StoreUser user)
        {
            order.OrderDate = DateTime.Now;
            order.OrderNumber = DateTime.Now.ToString();
            order.User = user;
            _wormDbContext.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderItem = new OrderItem()
                {
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price,
                    Amount = item.Amount
                };
                _wormDbContext.OrderItems.Add(orderItem);
            }

            _wormDbContext.SaveChanges();
        }
    }
}
