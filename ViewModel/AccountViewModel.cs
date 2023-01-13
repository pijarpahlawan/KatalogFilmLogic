using KatalogFilm.ViewModel.Helper;
using TMDbLib.Client;
using TMDbLib.Objects.Account;
using TMDbLib.Objects.Authentication;

namespace KatalogFilm.ViewModel
{
    internal class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {
            // get session
            _apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            _sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            _client = new TMDbClient(_apiKey);
            _client.SetSessionInformationAsync(_sessionID, SessionType.UserSession);
            _accountDetails = _client.AccountGetDetailsAsync().Result;
        }

        private TMDbClient _client;
        private readonly string _apiKey;
        private readonly string _sessionID;
        private AccountDetails _accountDetails;

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
