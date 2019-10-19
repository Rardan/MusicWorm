using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public OrderRepository(WormDbContext wormDbContext,
            ShoppingCart shoppingCart)
        {
            _wormDbContext = wormDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order, StoreUser user)
        {
            var transaction = _wormDbContext.Database.BeginTransaction();
            try
            {
                order.OrderDate = DateTime.Now;
                order.OrderNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                order.User = user;
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
                order.Condidtion = "Created";
                _wormDbContext.Add(order);

                var shoppingCartItems = _shoppingCart.ShoppingCartItems;

                foreach (var item in shoppingCartItems)
                {
                    var orderItem = new OrderItem()
                    {
                        ProductId = item.Product.Id,
                        OrderId = order.Id,
                        Price = item.Product.Price * item.Amount,
                        Amount = item.Amount
                    };
                    var itemStorage = _wormDbContext.Store.FirstOrDefault(s => s.ProductId == item.Product.Id);
                    itemStorage.Amount = itemStorage.Amount - orderItem.Amount;
                    _wormDbContext.Store.Update(itemStorage);

                    _wormDbContext.OrderItems.Add(orderItem);
                }
                _wormDbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
            }
            
        }

        public Order GetOrderByNumber(string orderNumber)
        {
            var order = _wormDbContext.Orders
                .Include(i => i.Items)
                .Include("Items.Product")
                .Include("Items.Product.Artist")
                .FirstOrDefault(n => n.OrderNumber == orderNumber);

            return order;
        }

        public IEnumerable<Order> GetOrdersByUser(StoreUser user)
        {
            var orders = _wormDbContext.Orders
                .Include(i => i.Items)

                .Where(o => o.User == user)
                .ToList();
            return orders;
        }
    }
}
