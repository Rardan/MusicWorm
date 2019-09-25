using System.Collections.Generic;
using MusicWorm.Models;

namespace MusicWorm.Data
{
    public interface IMusicRepository
    {
        void AddEntity(object model);
        void AddOrder(Order newOrder);
        int DecrementQuantity(int id);
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        IEnumerable<Product> GetAllProducts();
        Order GetOrderById(string username, int id);
        int GetQuantity(int id);
        int IncrementQuantity(int id);
        bool SaveAll();
    }
}