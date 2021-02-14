using Microsoft.AspNetCore.Mvc;
using MovieShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        //constructor that takes in object of class that implements IMovieService. That class is MovieService.
        public MoviesController(IMovieService movieService) 
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()  //IActionResult ???
        {
            //Without parameter, it renders Details.cshtml by default.
            return View();

            //specify which view to render. testing.cshtml
            //return View("testing");
        }

        [HttpGet]
        public IActionResult TopRevenueMovies()
        {
            var movies = _movieService.GetHighestGrossingMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult TopRatedMovies()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Genre(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reviews(int id)
        {
            return View();
        }

    }
}