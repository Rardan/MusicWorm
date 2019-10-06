using Microsoft.EntityFrameworkCore;
using MusicWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly WormDbContext _wormDbContext;

        public ProductRepository(WormDbContext wormDbContext)
        {
            _wormDbContext = wormDbContext;
        }

        public IEnumerable<Product> Products => _wormDbContext.Products.Include(s => s.Storage).Include(a => a.Artist);
        public Product GetProductById(int productId) => _wormDbContext.Products.FirstOrDefault(p => p.Id == productId);
    }
}
