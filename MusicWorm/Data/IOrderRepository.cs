using MusicWorm.Models;

namespace MusicWorm.Data
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order, StoreUser user);
    }
}