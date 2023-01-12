using KatalogFilm.ViewModel.Helper;
using TMDbLib.Client;
using TMDbLib.Objects.Account;
using TMDbLib.Objects.Authentication;

namespace KatalogFilm.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            // get session
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
