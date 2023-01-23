using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace KatalogFilm.ViewModel
{
    public class DetailMovieViewModel : ViewModelBase
    {
        public DetailMovieViewModel(int id)
        {
            _apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            Client = new TMDbClient(_apiKey);
            CurrentMovie = new MovieObservable();
            _ = GetMovie(id);
        }

        private readonly string _apiKey;
        private MovieObservable _currentMovie;

        public TMDbClient Client { get; set; }
        public MovieObservable CurrentMovie
        {
            get => _currentMovie;
            set
            {
                _currentMovie = value;
                OnPropertyChanged(nameof(CurrentMovie));
            }
        }

        public async Task GetMovie(int id)
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            var movie = await Client.GetMovieAsync(id);
            CurrentMovie.PosterPath = endpoint + movie.PosterPath;
            CurrentMovie.OriginalTitle = movie.OriginalTitle;
        }
    }
}
