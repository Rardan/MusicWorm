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
