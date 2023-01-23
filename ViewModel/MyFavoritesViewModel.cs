﻿using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace KatalogFilm.ViewModel
{
    public class MyFavoritesViewModel : ViewModelBase
    {
        public MyFavoritesViewModel()
        {
            // get session
            //_apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            //_sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            //_client = new TMDbClient(_apiKey);
            //_client.SetSessionInformationAsync(_sessionID, SessionType.UserSession);
            CurrentPage = 1;
            SearchedMovies = new ObservableCollection<MovieObservable>();
            _ = GetMovies();
        }

        //private TMDbClient _client;
        //private readonly string _apiKey;
        //private readonly string _sessionID;
        private ObservableCollection<MovieObservable> _searchedMovies;
        private int _currentPage;
        private int _totalPage;
        private ICommand _nextCommand;
        private ICommand _previousCommand;


        //public TMDbClient Client
        //{
        //    get => _client;
        //    set
        //    {
        //        _client.Dispose();
        //        _client = value;
        //    }
        //}
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

        public ObservableCollection<MovieObservable> SearchedMovies
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
                _nextCommand ??= new RelayCommand(async param => await GoToNextPage(), param => CanGoToNextPageExecuted());
                return _nextCommand;
            }
        }
        public ICommand PreviousCommand1
        {
            get
            {
                _previousCommand ??= new RelayCommand(async param => await GoToPreviousPage(), param => CanGoToPreviousPageExecuted());
                return _previousCommand;
            }
        }

        public async Task GetMovies()
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            //var user = await _client.AccountGetDetailsAsync();
            var movies = await App.Client.AccountGetFavoriteMoviesAsync(page: CurrentPage);
            TotalPage = movies.TotalPages;
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
                    Poster = ImageBrushConverter.PathToImageBrush(endpoint + item.PosterPath)
                });
            }
        }
        public async Task GoToNextPage()
        {
            CurrentPage++;
            await GetMovies();
        }
        public bool CanGoToNextPageExecuted()
        {
            return CurrentPage < TotalPage;
        }
        public async Task GoToPreviousPage()
        {
            CurrentPage--;
            await GetMovies();
        }
        public bool CanGoToPreviousPageExecuted()
        {
            return CurrentPage > 1;
        }
    }
}
