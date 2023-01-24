using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace KatalogFilm.ViewModel
{
    // view model untuk page home
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
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
        // keyword pencarian movie
        public string? KeywordSearch
        {
            get => _keywordSearch;
            set
            {
                _keywordSearch = value;
                CurrentPage = 0;
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
        // perintah untuk tombol next
        public ICommand NextCommand
        {
            get
            {
                _nextCommand ??= new RelayCommand(async param => await GoToNextPage(), param => CanGoToNextPageExecuted());
                return _nextCommand;
            }
        }
        // perintah untuk tombol prev
        public ICommand PreviousCommand
        {
            get
            {
                _previousCommand ??= new RelayCommand(async param => await GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }
        // perintah ketika memilih salah satu movie
        public ICommand SelectMovieCommand
        {
            get
            {
                _selectMovieCommand ??= new RelayCommand(param => GetDetailMovie(param), null);
                return _selectMovieCommand;
            }
        }
        // perintah ketika melakukan pencarian movie
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
            SearchedMovies.Clear();
            const string endpoint = "https://image.tmdb.org/t/p/original";
            SearchContainer<SearchMovie> movies = new SearchContainer<SearchMovie>();

            // apabila tidak dilakukan pencarian, maka mendapatkan movie top rated
            if (string.IsNullOrEmpty(KeywordSearch) || string.IsNullOrWhiteSpace(KeywordSearch))
            {
                movies = await App.Client.GetMoviePopularListAsync(page: CurrentPage);
            }
            // apabila dilakukan pencarian maka mendapatkan daftar movie sesuai keyword
            else
            {
                movies = await App.Client.SearchMovieAsync(KeywordSearch, page: CurrentPage);
            }

            TotalPage = movies.TotalPages;

            // mendapatkan movie
            foreach (var item in movies.Results)
            {
                SearchedMovies.Add(new MovieObservable
                {
                    Id = item.Id,
                    Adult = item.Adult,
                    OriginalTitle = item.OriginalTitle,
                    OriginalLanguage = item.OriginalLanguage,
                    Overview = item.Overview,
                    PosterPath = endpoint + item.PosterPath,
                    Poster = ImageBrushConverter.PathToImageBrush(endpoint + item.PosterPath)
                });
            }
        }
        // fungsi untuk ke page movie selanjutnya
        public async Task GoToNextPage()
        {
            CurrentPage++;
            await GetMovies();
        }
        // fungsi untuk menentukan apakah tombol next dapat diklik
        public bool CanGoToNextPageExecuted()
        {
            return CurrentPage < TotalPage;
        }
        // fungsi untuk ke page movie sebelumnya
        public async Task GoToPreviousPage()
        {
            CurrentPage--;
            await GetMovies();
        }
        // fungsi untuk menentukan apakah tombol prev dapat diklik
        public bool CanGoToPreviousPageExecuted()
        {
            return CurrentPage > 1;
        }
        // fungsi untuk membuka window detail movie yang diklik
        public void GetDetailMovie(object id)
        {
            var detailMovie = new DetailMovie();
            detailMovie.DataContext = new DetailMovieViewModel(Convert.ToInt32(id));
            detailMovie.Show();
        }
    }
}
