using System.Windows.Input;

namespace KatalogFilm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }
        public ICommand HomeCommand { get; set; }
        public ICommand MyFavoriteCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public bool IsMainVisible
        {
            get => _isMainVisible;
            set
            {
                _isMainVisible = value;
                OnPropertyChanged(nameof(IsMainVisible));
            }
        }

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void MyFavorite(object obj) => CurrentView = new MyFavoritesViewModel();
        private void Account(object obj) => CurrentView = new AccountViewModel();
        private void Login(object obj) => CurrentView = new LoginViewModel();

        private bool _isMainVisible;
        public MainViewModel()
        {
            HomeCommand = new RelayCommand(Home, null);
            MyFavoriteCommand = new RelayCommand(MyFavorite, null);
            AccountCommand = new RelayCommand(Account, null);
            LoginCommand = new RelayCommand(Login, null);

            //start page
            CurrentView = new HomeViewModel();
            IsMainVisible = true;
        }
        /*
        public MainViewModel()
        {
            apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            _client = new TMDbClient(apiKey);
            _client.SetSessionInformationAsync(sessionID, SessionType.UserSession);
            _accountDetails = _client.AccountGetDetailsAsync().Result;
        }

        private TMDbClient _client;
        private readonly string apiKey;
        private readonly string sessionID;
        private AccountDetails _accountDetails;

        
        public AccountDetails AccountDetails
        {
            get => _accountDetails;
            set
            {
                _accountDetails = value;
                OnPropertyChanged(nameof(AccountDetails));
            }
        }*/
    }
}
