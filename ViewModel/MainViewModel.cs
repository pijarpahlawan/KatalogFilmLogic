using KatalogFilm.ViewModel.Helper;
using TMDbLib.Client;
using TMDbLib.Objects.Account;
using TMDbLib.Objects.Authentication;

namespace KatalogFilm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            _isMainVisible = true;
            _client = new TMDbClient(apiKey);
            _client.SetSessionInformationAsync(sessionID, SessionType.UserSession);
            _accountDetails = _client.AccountGetDetailsAsync().Result;
        }

        private bool _isMainVisible;
        private TMDbClient _client;
        private AccountDetails _accountDetails;
        private readonly string apiKey;
        private readonly string sessionID;

        public bool IsMainVisible
        {
            get => _isMainVisible;
            set
            {
                _isMainVisible = value;
                OnPropertyChanged(nameof(IsMainVisible));
            }
        }

        public TMDbClient Client
        {
            get => _client;
            set
            {
                _client.Dispose();
                _client = value;
            }
        }
        public AccountDetails AccountDetails
        {
            get => _accountDetails;
            set
            {
                _accountDetails = value;
                OnPropertyChanged(nameof(AccountDetails));
            }
        }
    }
}
