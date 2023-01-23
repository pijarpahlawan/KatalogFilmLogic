using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System.Threading.Tasks;

namespace KatalogFilm.ViewModel
{
    // view model untuk detail movie
    public class DetailMovieViewModel : ViewModelBase
    {
        public DetailMovieViewModel(int id)
        {
            CurrentMovie = new MovieObservable();
            _ = GetMovie(id);
        }

        private MovieObservable _currentMovie;

        // movie yang terpilih saat ini dalam bentuk observable model
        public MovieObservable CurrentMovie
        {
            get => _currentMovie;
            set
            {
                _currentMovie = value;
                OnPropertyChanged(nameof(CurrentMovie));
            }
        }

        // mendapatkan detail movie saat ini
        public async Task GetMovie(int id)
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            var movie = await App.Client.GetMovieAsync(id);
            CurrentMovie.Id = movie.Id;
            CurrentMovie.Adult = movie.Adult;
            CurrentMovie.OriginalTitle = movie.OriginalTitle;
            CurrentMovie.OriginalLanguage = movie.OriginalLanguage;
            CurrentMovie.Overview = movie.Overview;
            CurrentMovie.PosterPath = endpoint + movie.PosterPath;
            CurrentMovie.Poster = ImageBrushConverter.PathToImageBrush(endpoint + movie.PosterPath);
        }
    }
}
