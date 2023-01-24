using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace KatalogFilm.ViewModel
{
    // view model untuk page favorite
    public class MyFavoritesViewModel : ViewModelBase
    {
        public MyFavoritesViewModel()
        {
            CurrentPage = 1;
            SearchedMovies = new ObservableCollection<MovieObservable>();
            _ = GetMovies();
        }

        private ObservableCollection<MovieObservable> _searchedMovies;
        private string? _keywordSearch;
        private int _currentPage;
        private int _totalPage;
        private ICommand _nextCommand;
        private ICommand _previousCommand;
        private ICommand _selectMovieCommand;
        private ICommand _searchMovieCommand;

        // page movie saat ini
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        // total page movie
        public int TotalPage
        {
            get => _totalPage;
            set
            {
                _totalPage = value;
                OnPropertyChanged(nameof(TotalPage));
            }
        }
        // keyword pencarian
        public string? KeywordSearch
        {
            get => _keywordSearch;
            set
            {
                _keywordSearch = value;
                OnPropertyChanged(nameof(KeywordSearch));
            }
        }
        // kumpulan movie
        public ObservableCollection<MovieObservable> SearchedMovies
        {
            get => _searchedMovies;
            set
            {
                _searchedMovies = value;
                OnPropertyChanged(nameof(SearchedMovies));
            }
        }
        // perintah untuk menuju page berikutnya
        public ICommand NextCommand
        {
            get
            {
                _nextCommand ??= new RelayCommand(async param => await GoToNextPage(), param => CanGoToNextPageExecuted());
                return _nextCommand;
            }
        }
        // perintah untuk menuju page sebelumnya
        public ICommand PreviousCommand
        {
            get
            {
                _previousCommand ??= new RelayCommand(async param => await GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }
        // perintah pada tiap tombol movie
        public ICommand SelectMovieCommand
        {
            get
            {
                _selectMovieCommand ??= new RelayCommand(param => GetDetailMovie(param), null);
                return _selectMovieCommand;
            }
        }
        // perintah pada tombol pencarian
        public ICommand SearchMovieCommand
        {
            get
            {
                _searchMovieCommand ??= new RelayCommand(async param => await GetMovies(), null);
                return _searchMovieCommand;
            }
        }

        // fungsi untuk mendapatkan daftar movie
        public async Task GetMovies()
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            var movies = await App.Client.AccountGetFavoriteMoviesAsync(page: CurrentPage);
            TotalPage = movies.TotalPages;

            // mendapatkan movie
            foreach (var item in movies.Results)
            {
                SearchedMovies.Add(new MovieObservable
                {
                    Adult = item.Adult,
                    Id = item.Id,
                    OriginalLanguage = item.OriginalLanguage,
                    OriginalTitle = item.OriginalTitle,
                    Overview = item.Overview,
                    PosterPath = endpoint + item.PosterPath,
                    Poster = Converter.PathToImageBrush(endpoint + item.PosterPath)
                });
            }
        }
        // fungsi untuk menuju page berikutnya
        public async Task GoToNextPage()
        {
            CurrentPage++;
            await GetMovies();
        }
        // fungsi untuk menentukan tombol next bisa diklik atau tidak
        public bool CanGoToNextPageExecuted()
        {
            return CurrentPage < TotalPage;
        }
        // fungsi untuk menuju page sebelumnya
        public async Task GoToPreviousPage()
        {
            CurrentPage--;
            await GetMovies();
        }
        // fungsi untuk menentukan tombol prev bisa diklik atau tidak
        public bool CanGoToPreviousPageExecuted()
        {
            return CurrentPage > 1;
        }
        // fungsi untuk mendapatkan detail movie
        public void GetDetailMovie(object id)
        {
            var detailMovie = new DetailMovie();
            detailMovie.DataContext = new DetailMovieViewModel(Convert.ToInt32(id));
            detailMovie.Show();
        }
    }
}
