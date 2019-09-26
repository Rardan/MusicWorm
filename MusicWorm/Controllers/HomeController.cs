﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MusicWorm.Data;
using MusicWorm.Models;
using MusicWorm.Services;
using MusicWorm.ViewModels;

namespace MusicWorm.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _mailService;
        private readonly IMusicRepository _repository;

        public HomeController(IEmailSender mailService, IMusicRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //_mailService.SendMessage("contact@musicworm.com", model.Subject, $"From {model.Name} - {model.Email}, Message: {model.Message}");
                _mailService.SendEmailAsync("contact@musicworm.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Shop")]
        public IActionResult Shop()
        {
            var products = _repository.GetAllProducts();
            return View(products);
        }
    }
}
