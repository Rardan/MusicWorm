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

        public IEnumerable<Order> Orders => _wormDbContext.Orders.OrderBy(o => o.OrderNumber);

        public void ChangeOrderStatus(Order order, string state)
        {
            if (order != null)
            {
                order.Condidtion = state;
                _wormDbContext.Update(order);
                _wormDbContext.SaveChanges();
            }
        }

        public void CreateOrder(Order order, StoreUser user)
        {
            //var transaction = _wormDbContext.Database.BeginTransaction();
            //try
            //{
            order.OrderDate = DateTime.Now;
            order.OrderNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
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
                //    var itemStorage = _wormDbContext.Store.FirstOrDefault(s => s.ProductId == item.Product.Id);
                //    itemStorage.Amount = itemStorage.Amount - orderItem.Amount;
                //    _wormDbContext.Store.Update(itemStorage);

                _wormDbContext.OrderItems.Add(orderItem);
            }
            _wormDbContext.SaveChanges();
            //    transaction.Commit();
            //}
            //catch (Exception e)
            //{
            //    transaction.Rollback();
            //}
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

        public IEnumerable<string> GetOrderNumbers()
        {
            var numbers = _wormDbContext.Orders.Select(o => o.OrderNumber).ToList();
            return numbers;
        }

        public void DeleteOrder(Order order)
        {
            _wormDbContext.Remove(order);
            _wormDbContext.SaveChanges();
        }
    }
}
