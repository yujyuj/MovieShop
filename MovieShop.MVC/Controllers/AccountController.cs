using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int id)
        {
            return View();
        }
    }
}
