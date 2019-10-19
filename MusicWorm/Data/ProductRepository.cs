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

        public void IncreaseInStorage(int productId)
        {
            var storage = _wormDbContext.Store.FirstOrDefault(i => i.ProductId == productId);

            if (storage != null)
            {
                storage.Amount++;
                _wormDbContext.Update(storage);
                _wormDbContext.SaveChanges();
            }
        }

        public void DecreaseInStorage(int productId)
        {
            var storage = _wormDbContext.Store.FirstOrDefault(i => i.ProductId == productId);
            if (storage != null)
            {
                if (storage.Amount > 0)
                {
                    storage.Amount--;
                    _wormDbContext.Update(storage);
                    _wormDbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Storage> Storages => _wormDbContext.Store.Include(p => p.Product).ThenInclude(a => a.Artist);

        
    }
}
