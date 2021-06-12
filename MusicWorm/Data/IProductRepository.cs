using System.Collections.Generic;
using MusicWorm.Models;

namespace MusicWorm.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Storage> Storages { get; }

        Product GetProductById(int productId);

        void IncreaseInStorage(int productId);
        void DecreaseInStorage(int productId);
        Product CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}