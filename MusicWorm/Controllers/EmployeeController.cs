using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicWorm.Data;

namespace MusicWorm.Controllers
{
    [Authorize(Roles ="Employee,Owner,Admin")]
    public class EmployeeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public EmployeeController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageStorage()
        {
            var storage = _productRepository.Storages.OrderBy(p => p.Product.Title);
            return View(storage);
        }

        public RedirectToActionResult IncreaseAmountInStore(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.Id == productId);
            if(selectedProduct != null)
            {
                _productRepository.IncreaseInStorage(productId);
            }

            return RedirectToAction("ManageStorage");
        }

        public RedirectToActionResult DecreaseAmountInStore(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.Id == productId);
            if (selectedProduct != null)
            {
                _productRepository.DecreaseInStorage(productId);
            }

            return RedirectToAction("ManageStorage");
        }

        public IActionResult ManageOrderStatus()
        {
            var orders = _orderRepository.Orders;

            return View(orders);
        }

        public RedirectToActionResult ChangeOrderStatus(string orderNumber)
        {
            var order = _orderRepository.GetOrderByNumber(orderNumber);

            if (order != null)
            {
                if(order.Condidtion == "Created")
                {
                    _orderRepository.ChangeOrderStatus(order, "Packed");
                }
                else if (order.Condidtion == "Packed")
                {
                    _orderRepository.ChangeOrderStatus(order, "Shipped");
                }
                
            }

            return RedirectToAction("ManageOrderStatus");
        }
    }
}