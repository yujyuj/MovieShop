using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _movieRepository; //cannot be changed after being assigned in the constructor
        public MovieService(IMovieRepository movieRepository) //constructor
        {
            _movieRepository = movieRepository;
        }
        public IEnumerable<Movie> GetHighestGrossingMovies()
        {
            var movies = _movieRepository.GetTopRevenueMovies(); 
            return movies;
        }

    }

}