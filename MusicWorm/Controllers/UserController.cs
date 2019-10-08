using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicWorm.Data;
using MusicWorm.Models;

namespace MusicWorm.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public UserController(UserManager<StoreUser> userManager,
            IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var orders = _orderRepository.GetOrdersByUser(user);
            return View(orders);
        }
    }
}