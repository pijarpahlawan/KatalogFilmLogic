using KatalogFilm.ViewModel.ObservableModel;

namespace KatalogFilm.ViewModel
{
    internal class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {
            // get session
            //_apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            //_sessionID = RWJson.ReadFromJSON("session-id", "session.json");
            //_client = new TMDbClient(_apiKey);
            //_client.SetSessionInformationAsync(_sessionID, SessionType.UserSession);
            AccountObservable = new AccountObservable();
            GetAccountDetails();
        }

        //private TMDbClient _client;
        //private readonly string _apiKey;
        //private readonly string _sessionID;
        private AccountObservable _accountObservable;

        //public TMDbClient Client
        //{
        //    get => _client;
        //    set
        //    {
        //        _client.Dispose();
        //        _client = value;
        //    }
        //}
        public AccountObservable AccountObservable
        {
            get => _accountObservable;
            set
            {
                _accountObservable = value;
                OnPropertyChanged(nameof(AccountObservable));
            }
        }
        public void GetAccountDetails()
        {
            //var accountDetails = await _client.AccountGetDetailsAsync();
            AccountObservable.Id = App.Account.Id;
            AccountObservable.Name = App.Account.Name;
            AccountObservable.Username = App.Account.Username;
            AccountObservable.IncludeAdult = App.Account.IncludeAdult == true ? "Dewasa" : "Non Dewasa";
            AccountObservable.AvatarUrl = "https://www.gravatar.com/avatar/" + App.Account.Avatar.Gravatar.Hash;
        }
    }
}
