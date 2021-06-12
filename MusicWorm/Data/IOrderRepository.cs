using MusicWorm.Models;
using System.Collections.Generic;

namespace MusicWorm.Data
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order, StoreUser user);
        IEnumerable<Order> GetOrdersByUser(StoreUser user);
        IEnumerable<string> GetOrderNumbers();
        IEnumerable<Order> Orders { get; }

        Order GetOrderByNumber(string orderNumber);

        void ChangeOrderStatus(Order order, string state);
        void DeleteOrder(Order order);
    }
}