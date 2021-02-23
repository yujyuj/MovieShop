using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {

        //call this method when user clicks on Buy Movie in Movie details page
        //Filters [Authorization]
        [HttpGet]
        //[Authorization]
        public IActionResult BuyMovie()
        {
            //call UserService to save the movie that will call repository that will save in purchase table
            return View();
        }
    }
}
