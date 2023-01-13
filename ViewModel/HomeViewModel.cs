using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TMDbLib.Client;
using TMDbLib.Objects.Authentication;
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
        }

        private TMDbClient _client;
        private readonly string _apiKey;
        private readonly string _sessionID;
        private ObservableCollection<SearchMovie> _searchedMovies;
        private int _currentPage;
        private int _totalPage;
        private ICommand _nextCommand;
        private ICommand _previousCommand;
        private ICommand _selectMovie;


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

        public ObservableCollection<SearchMovie> SearchedMovies
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
        public ICommand PreviousCommand1
        {
            get
            {
                _previousCommand ??= new RelayCommand(param => GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }
        public ICommand SelectMovie
        {
            get
            {
                _selectMovie ??= new RelayCommand(param => GetDetailMovie(), null);
            }
        }

        public async void GetMovies()
        {
            var movies = await _client.GetMovieTopRatedListAsync(page: CurrentPage);
            TotalPage = movies.TotalPages;
            foreach (var item in movies.Results)
            {
                SearchedMovies.Add(new SearchMovie
                {
                    Adult = item.Adult,
                    BackdropPath = item.BackdropPath,
                    GenreIds = item.GenreIds,
                    Id = item.Id,
                    MediaType = item.MediaType,
                    OriginalLanguage = item.OriginalLanguage,
                    OriginalTitle = item.OriginalTitle,
                    Overview = item.Overview,
                    Popularity = item.Popularity,
                    PosterPath = item.PosterPath,
                    ReleaseDate = item.ReleaseDate,
                    Title = item.Title,
                    Video = item.Video,
                    VoteAverage = item.VoteAverage,
                    VoteCount = item.VoteCount,
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
        public void GetDetailMovie()
        {
            var detailMovie = new SelectedMovie();
            detailMovie.DataContext = new SelectedMovieViewModel();
        }
    }
}
