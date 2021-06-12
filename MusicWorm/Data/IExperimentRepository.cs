using MusicWorm.Models;
using MusicWorm.ViewModels;

namespace MusicWorm.Data
{
    public interface IExperimentRepository
    {
        ResultViewModel AddToShoppingCart();
        PartialResult AddToShoppingCart(int times);
        ResultViewModel CreateOrder();
        PartialResult CreateOrder(int times);
        ResultViewModel CreateProduct();
        PartialResult CreateProduct(int times);
        ResultViewModel GetAllOrders();
        PartialResult GetAllOrders(int times);
        ResultViewModel GetOrderByNumber();
        PartialResult GetOrderByNumber(int times);
        void PrepareData();
        void PrepareArtists();
        void PrepareProducts();
        void PrepareOrders();
        void DeleteAllData();
    }
}