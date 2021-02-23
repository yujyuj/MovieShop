﻿using Microsoft.AspNetCore.Mvc; //ViewComponent
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Views.Shared.Components.Genres
{
    public class GenresViewComponent :ViewComponent
    {
        private readonly IGenreService _genreService;
        public GenresViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres); //by default, it renders default.cshtml inside the same folder
        }
    }
}
