using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MusicWorm.Models;
using MusicWorm.Utils;
using MusicWorm.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Data
{
    public class ExperimentRepository : IExperimentRepository
    {
        private readonly int iterations = 20;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Stopwatch stopwatch;

        public ExperimentRepository(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IArtistRepository artistRepository,
            ShoppingCart shoppingCart,
            UserManager<StoreUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _artistRepository = artistRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeleteAllData()
        {
            _shoppingCart.ClearCart();

            var orders = _orderRepository.Orders.ToList();
            foreach (var item in orders)
            {
                _orderRepository.DeleteOrder(item);
            }

            var products = _productRepository.Products.ToList();
            foreach (var item in products)
            {
                _productRepository.DeleteProduct(item);
            }

            var artists = _artistRepository.Artists.ToList();
            foreach (var item in artists)
            {
                _artistRepository.DeleteArtist(item);
            }
        }

        public void PrepareData()
        {
            var artists = _artistRepository.Artists.ToList();
            if (artists.Count() <= 10)
            {
                for (int i = 0; i < 100; i++)
                {
                    _artistRepository.CreateArtist(PrepareArtist());
                }
            }

            var products = _productRepository.Products.ToList();
            if (products.Count() <= 10)
            {
                for (int i = 0; i < 100; i++)
                {
                    _productRepository.CreateProduct(PrepareProduct());
                }
            }

            var orders = _orderRepository.Orders.ToList();
            if (orders.Count() <= 10)
            {
                var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                for (int i = 0; i < 1000; i++)
                {
                    var order = PrepareOrder();
                    _orderRepository.CreateOrder(PrepareOrder(), user);
                }
            }
        }

        private Artist PrepareArtist()
        {
            Artist artist = new Artist()
            {
                Name = StringGeneratorUtil.GenerateRandomString(),
                Country = StringGeneratorUtil.GenerateRandomString()
            };
            return artist;
        }

        private Product PrepareProduct()
        {
            var artists = _artistRepository.Artists.ToList();
            var artist = artists.ElementAt(RandomNumberUtil.GenerateFromRange(0, artists.Count()));
            Product product = new Product()
            {
                Title = StringGeneratorUtil.GenerateRandomString(),
                Genre = StringGeneratorUtil.GenerateRandomString(),
                Price = 10,
                AlbumDescription = StringGeneratorUtil.GenerateRandomString(),
                ArtistId = artist.Id,
                AlbumDating = DateTime.Now
            };
            return product;
        }

        private Order PrepareOrder()
        {
            _shoppingCart.ClearCart();
            var products = _productRepository.Products.ToList();
            var product = products.ElementAt(RandomNumberUtil.GenerateFromRange(0, products.Count()));
            _shoppingCart.AddToCart(product, 1);
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            Order order = new Order()
            {
                FirstName = StringGeneratorUtil.GenerateRandomString(),
                LastName = StringGeneratorUtil.GenerateRandomString(),
                AdressLine1 = StringGeneratorUtil.GenerateRandomString(),
                ZipCode = "12345",
                City = StringGeneratorUtil.GenerateRandomString(),
                Country = StringGeneratorUtil.GenerateRandomString(),
                PhoneNumber = "123456789",
                Email = "test@test.com"
            };
            return order;
        }

        public ResultViewModel CreateProduct()
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = nameof(CreateProduct),
                Result1 = CreateProduct(1),
                Result10 = CreateProduct(10),
                Result20 = CreateProduct(20),
                Result50 = CreateProduct(50),
                Result100 = CreateProduct(100)
            };
            return result;
        }

        public PartialResult CreateProduct(int times)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < iterations; i++)
            {
                List<double> partial = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    Product product = PrepareProduct();
                    stopwatch = Stopwatch.StartNew();
                    var newProduct = _productRepository.CreateProduct(product);
                    stopwatch.Stop();
                    partial.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(partial.Sum());
            }
            return ResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel AddToShoppingCart()
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = nameof(AddToShoppingCart),
                Result1 = AddToShoppingCart(1),
                Result10 = AddToShoppingCart(10),
                Result20 = AddToShoppingCart(20),
                Result50 = AddToShoppingCart(50),
                Result100 = AddToShoppingCart(100)
            };
            return result;
        }

        public PartialResult AddToShoppingCart(int times)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < iterations; i++)
            {
                List<double> partial = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    _shoppingCart.ClearCart();
                    var products = _productRepository.Products;
                    var product = products.ElementAt(RandomNumberUtil.GenerateFromRange(0, products.Count()));
                    stopwatch = Stopwatch.StartNew();
                    _shoppingCart.AddToCart(product, 1);
                    stopwatch.Stop();
                    partial.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(partial.Sum());
            }
            return ResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel CreateOrder()
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = nameof(CreateOrder),
                Result1 = CreateOrder(1),
                Result10 = CreateOrder(10),
                Result20 = CreateOrder(20),
                Result50 = CreateOrder(50),
                Result100 = CreateOrder(100)
            };
            return result;
        }

        public PartialResult CreateOrder(int times)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < iterations; i++)
            {
                List<double> partial = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    Order order = PrepareOrder();
                    var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                    var user = _userManager.FindByNameAsync(userName).Result;
                    stopwatch = Stopwatch.StartNew();
                    _orderRepository.CreateOrder(order, user);
                    stopwatch.Stop();
                    partial.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(partial.Sum());
            }
            return ResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel GetOrderByNumber()
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = nameof(GetOrderByNumber),
                Result1 = GetOrderByNumber(1),
                Result10 = GetOrderByNumber(10),
                Result20 = GetOrderByNumber(20),
                Result50 = GetOrderByNumber(50),
                Result100 = GetOrderByNumber(100)
            };
            return result;
        }

        public PartialResult GetOrderByNumber(int times)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < iterations; i++)
            {
                List<double> partial = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _orderRepository.GetOrderNumbers();
                    var selectedOrder = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var order = _orderRepository.GetOrderByNumber(selectedOrder);
                    stopwatch.Stop();
                    partial.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(partial.Sum());
            }
            return ResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel GetAllOrders()
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = nameof(GetAllOrders),
                Result1 = GetAllOrders(1),
                Result10 = GetAllOrders(10),
                Result20 = GetAllOrders(20),
                Result50 = GetAllOrders(50),
                Result100 = GetAllOrders(100)
            };
            return result;
        }

        public PartialResult GetAllOrders(int times)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < iterations; i++)
            {
                List<double> partial = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _orderRepository.Orders.ToList();
                    stopwatch.Stop();
                    partial.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(partial.Sum());
            }
            return ResultUtil.CreatePartialResult(results);
        }
    }
}
