using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Data
{
    public class MusicRepository : IMusicRepository
    {
        private readonly WormDbContext _ctx;
        private readonly ILogger<MusicRepository> _logger;

        public MusicRepository(WormDbContext ctx, ILogger<MusicRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = _ctx.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                    .ToList();
            }
        }

        public  IEnumerable<Order> GetAllOrdersByUser (string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts were called");
                return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName == username)
                .FirstOrDefault();
        }

        public int GetQuantity(int id)
        {
            return _ctx.Store
                .Where(s => s.ProductId == id)
                .FirstOrDefault()
                .Quantity;
        }

        public int IncrementQuantity(int id)
        {
            return _ctx.Store
                .Where(s => s.ProductId == id)
                .FirstOrDefault()
                .Quantity++;
        }

        public int DecrementQuantity(int id)
        {
            if (GetQuantity(id) > 0)
            {
                return _ctx.Store
                .Where(s => s.ProductId == id)
                .FirstOrDefault()
                .Quantity--;
            }
            else
            {
                return GetQuantity(id);
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
