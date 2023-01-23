using KatalogFilm.Model;
using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TMDbLib.Client;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace KatalogFilm.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            // get session
            _apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            _sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            _client = new TMDbClient(_apiKey);
            _client.SetSessionInformationAsync(_sessionID, SessionType.UserSession);
            CurrentPage = 0;
            SearchedMovies = new ObservableCollection<Movie>();
            GetMovies();
        }

        private TMDbClient _client;
        private readonly string _apiKey;
        private readonly string _sessionID;
        private string? _keywordSearch;
        private ObservableCollection<Movie> _searchedMovies;
        private int _currentPage;
        private int _totalPage;
        private ICommand _nextCommand;
        private ICommand _previousCommand;
        private ICommand _selectMovieCommand;
        private ICommand _searchMovieCommand;


        public TMDbClient Client
        {
            get => _client;
            set
            {
                _client.Dispose();
                _client = value;
            }
        }
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public int TotalPage
        {
            get => _totalPage;
            set
            {
                _totalPage = value;
                OnPropertyChanged(nameof(TotalPage));
            }
        }
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
        public ObservableCollection<Movie> SearchedMovies
        {
            get => _searchedMovies;
            set
            {
                _searchedMovies = value;
                OnPropertyChanged(nameof(SearchedMovies));
            }
        }
        public ICommand NextCommand
        {
            get
            {
                _nextCommand ??= new RelayCommand(param => GoToNextPage(), param => CanGoToNextPageExecuted());
                return _nextCommand;
            }
        }
        public ICommand PreviousCommand
        {
            get
            {
                _previousCommand ??= new RelayCommand(param => GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }
        public ICommand SelectMovieCommand
        {
            get
            {
                _selectMovieCommand ??= new RelayCommand(param => GetDetailMovie(param), null);
                return _selectMovieCommand;
            }
        }
        public ICommand SearchMovieCommand
        {
            get
            {
                _searchMovieCommand ??= new RelayCommand(param => GetMovies(), null);
                return _searchMovieCommand;
            }
        }

        public async void GetMovies()
        {
            SearchedMovies.Clear();
            const string endpoint = "https://image.tmdb.org/t/p/original";
            SearchContainer<SearchMovie> movies = new SearchContainer<SearchMovie>();
            if (string.IsNullOrEmpty(KeywordSearch) || string.IsNullOrWhiteSpace(KeywordSearch))
            {
                movies = await _client.GetMovieTopRatedListAsync(page: CurrentPage);
            }
            else
            {
                movies = await _client.SearchMovieAsync(KeywordSearch, page: CurrentPage);
            }
            TotalPage = movies.TotalPages;
            foreach (var item in movies.Results)
            {
                SearchedMovies.Add(new Movie
                {
                    Adult = item.Adult,
                    BackdropPath = item.BackdropPath,
                    GenreIds = item.GenreIds,
                    Id = item.Id,
                    OriginalLanguage = item.OriginalLanguage,
                    OriginalTitle = item.OriginalTitle,
                    Overview = item.Overview,
                    Poster = ImageBrushConverter.PathToImageBrush(endpoint + item.PosterPath)
                });
            }
        }
        public void GoToNextPage()
        {
            CurrentPage++;
            GetMovies();
        }
        public bool CanGoToNextPageExecuted()
        {
            return CurrentPage < TotalPage;
        }
        public void GoToPreviousPage()
        {
            CurrentPage--;
            GetMovies();
        }
        public bool CanGoToPreviousPageExecuted()
        {
            return CurrentPage > 1;
        }
        public void GetDetailMovie(object id)
        {
            var detailMovie = new DetailMovie();
            detailMovie.DataContext = new DetailMovieViewModel(Convert.ToInt32(id));
            detailMovie.Show();
        }
    }
}
