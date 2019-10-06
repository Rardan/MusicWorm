using System.Collections.Generic;
using MusicWorm.Models;

namespace MusicWorm.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int productId);
    }
}