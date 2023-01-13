﻿using KatalogFilm.Model;
using KatalogFilm.ViewModel.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TMDbLib.Client;
using TMDbLib.Objects.Authentication;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;


namespace KatalogFilm.ViewModel
{
    public class MyFavoritesViewModel : ViewModelBase
    {
        public MyFavoritesViewModel()
        {
            // get session
            _apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            _sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            _client = new TMDbClient(_apiKey);
            _client.SetSessionInformationAsync(_sessionID, SessionType.UserSession);
            CurrentPage = 1;
            SearchedMovies = new ObservableCollection<Movie>();
            GetMovies();
        }

        private TMDbClient _client;
        private readonly string _apiKey;
        private readonly string _sessionID;
        private ObservableCollection<Movie> _searchedMovies;
        private int _currentPage;
        private int _totalPage;
        private ICommand _nextCommand;
        private ICommand _previousCommand;


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
        public ICommand PreviousCommand1
        {
            get
            {
                _previousCommand ??= new RelayCommand(param => GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }

        public void GetMovies()
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            if (_client.AccountGetFavoriteMoviesAsync(page: CurrentPage).Result is SearchContainer<SearchMovie> movies)
            {
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
    }
}
