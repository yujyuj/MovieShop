using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly MovieShopDbContext _dbContext;
        public GenresController(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {

            //linq
            //var genres = _dbContext.Genres.ToList();
            //var r = _dbContext.Genres.FirstOrDefault(g => g.Id == 200); //no 200 so return null

            //SingleOrDefault() throw exception when 1) more than 1 was found
            var result1 = _dbContext.Genres.SingleOrDefault(g => g.Id == 2);            // Name : Fantasy
            var result2 = _dbContext.Genres.SingleOrDefault(g => g.Id == 200);          // null because not found
            var result3 = _dbContext.Genres.SingleOrDefault(g => g.Name.Contains("a")); // exception because more than 1

            //Single() throw exception when 1) more than 1 was found  2) zero found
            var result4 = _dbContext.Genres.Single(g => g.Id == 2);                      // Name : Fantasy
            var result5 = _dbContext.Genres.Single(g => g.Id == 200);                    // exception because not found
            var result6 = _dbContext.Genres.Single(g => g.Name.Contains("a"));           // exception because more than 1

            return View();
        }
    }
}
