using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;    //cannot be changed after being assigned in the constructor
        public MovieService(IMovieRepository movieRepository)  //constructor
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsResponseModel> GetMovieById(int id)
        {
            var movieDetails = new MovieDetailsResponseModel();
            var movie = await _movieRepository.GetByIdAsync(id); // _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres).FirstOrDefault(m => m.Id == id);

            movieDetails.Id = movie.Id;
            movieDetails.PosterUrl = movie.PosterUrl;
            movieDetails.Title = movie.Title;
            movieDetails.Overview = movie.Overview;
            movieDetails.Tagline = movie.Tagline;
            movieDetails.Budget = movie.Budget;
            movieDetails.Revenue = movie.Revenue;
            movieDetails.ImdbUrl = movie.ImdbUrl;
            movieDetails.TmdbUrl = movie.TmdbUrl;
            movieDetails.BackdropUrl = movie.BackdropUrl;
            movieDetails.OriginalLanguage = movie.OriginalLanguage;
            movieDetails.ReleaseDate = movie.ReleaseDate;
            movieDetails.RunTime = movie.RunTime;
            movieDetails.Price = movie.Price;

            //movieDetails.Genres and movie.Genres are different type, so I think we cannot simply use =
            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
            }

            //movieDetails.Casts and movie.MovieCasts are different type, so I think we cannot simply use =
            movieDetails.Casts = new List<CastReponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastReponseModel
                { 
                    Id = cast.CastId,
                    Character = cast.Character,
                    Name = cast.Cast.Name,
                    ProfilePath = cast.Cast.ProfilePath});
                }
            
            return movieDetails;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTop25topGrossingMovies()
        {
            var movies = await _movieRepository.GetTopRevenueMovies();  //a list of 25 movies of top revenue
            var movieCardResponseModel = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                //createa movieCard and add it to the result
                var movieCard = new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Revenue = movie.Revenue,
                    PosterUrl = movie.PosterUrl
                };
                movieCardResponseModel.Add(movieCard);
            }          
            return movieCardResponseModel;
        } 


    }

}