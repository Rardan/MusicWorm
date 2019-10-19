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

        public EmployeeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}