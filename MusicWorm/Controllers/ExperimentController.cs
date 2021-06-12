using Microsoft.AspNetCore.Mvc;
using MusicWorm.Data;
using MusicWorm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Controllers
{
    public class ExperimentController : Controller
    {
        private readonly IExperimentRepository _experimentRepository;

        public ExperimentController(IExperimentRepository experimentRepository)
        {
           _experimentRepository = experimentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrepareArtists()
        {
            _experimentRepository.PrepareArtists();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PrepareProducts()
        {
            _experimentRepository.PrepareProducts();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PrepareOrders()
        {
            _experimentRepository.PrepareOrders();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateProduct(int times)
        {
            var result = _experimentRepository.CreateProduct(times);
            return View(result);
        }

        public IActionResult AddToShoppingCart(int times)
        {
            var result = _experimentRepository.AddToShoppingCart(times);
            return View(result);
        }

        public IActionResult CreateOrder(int times)
        {
            var result = _experimentRepository.CreateOrder(times);
            return View(result);
        }

        public IActionResult GetOrderByNumber(int times)
        {
            var result = _experimentRepository.GetOrderByNumber(times);
            return View(result);
        }

        public IActionResult GetAllOrders(int times)
        {
            var result = _experimentRepository.GetAllOrders(times);
            return View(result);
        }

        public IActionResult Results()
        {
           //_experimentRepository.DeleteAllData();
            _experimentRepository.PrepareData();

            var results = new List<ResultViewModel>();

            results.Add(_experimentRepository.CreateProduct());
            results.Add(_experimentRepository.AddToShoppingCart());
            results.Add(_experimentRepository.CreateOrder());
            results.Add(_experimentRepository.GetOrderByNumber());
            results.Add(_experimentRepository.GetAllOrders());

            return View(results);
        }
    }
}
