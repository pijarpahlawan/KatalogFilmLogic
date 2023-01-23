using KatalogFilm.ViewModel.Helper;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace KatalogFilm.ViewModel
{
    public class DetailMovieViewModel
    {
        public DetailMovieViewModel(int id)
        {
            _apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            Client = new TMDbClient(_apiKey);
            CurrentMovie = new Movie();
            GetMovie(id);
        }

        private readonly string _apiKey;
        public TMDbClient Client { get; set; }
        public Movie CurrentMovie { get; set; }

        public void GetMovie(int id)
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            CurrentMovie = Client.GetMovieAsync(id).Result;
            CurrentMovie.PosterPath = endpoint + CurrentMovie.PosterPath;
        }
    }
}
