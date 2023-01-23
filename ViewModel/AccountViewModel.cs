using KatalogFilm.ViewModel.Helper;
using KatalogFilm.ViewModel.ObservableModel;
using System.Threading.Tasks;
using TMDbLib.Client;
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
            AccountObservable = new AccountObservable();
            _ = GetAccountDetails();
        }

        private TMDbClient _client;
        private readonly string _apiKey;
        private readonly string _sessionID;
        private AccountObservable _accountObservable;

        public TMDbClient Client
        {
            get => _client;
            set
            {
                _client.Dispose();
                _client = value;
            }
        }
        public AccountObservable AccountObservable
        {
            get => _accountObservable;
            set
            {
                _accountObservable = value;
                OnPropertyChanged(nameof(AccountObservable));
            }
        }
        public async Task GetAccountDetails()
        {
            var accountDetails = await _client.AccountGetDetailsAsync();
            AccountObservable.Id = accountDetails.Id;
            AccountObservable.Name = accountDetails.Name;
            AccountObservable.Username = accountDetails.Username;
            AccountObservable.IncludeAdult = accountDetails.IncludeAdult == true ? "Dewasa" : "Non Dewasa";
        }
    }
}
