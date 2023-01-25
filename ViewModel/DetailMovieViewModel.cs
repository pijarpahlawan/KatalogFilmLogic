using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;

namespace KatalogFilm.ViewModel
{
    // view model untuk detail movie
    public class DetailMovieViewModel : ViewModelBase
    {
        public DetailMovieViewModel(int id)
        {
            _currentMovie = new MovieObservable();
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
            var movie = await App.Client.GetMovieAsync(id, extraMethods: (MovieMethods)int.MaxValue);
            CurrentMovie = movie.ToMovieObservable();
        }
    }
}
