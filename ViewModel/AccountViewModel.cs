using KatalogFilm.ViewModel.ObservableModel;

namespace KatalogFilm.ViewModel
{
    // view model untuk page account
    internal class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {
            AccountObservable = new AccountObservable();
            GetAccountDetails();
        }

        private AccountObservable _accountObservable;

        // model observable dari account
        public AccountObservable AccountObservable
        {
            get => _accountObservable;
            set
            {
                _accountObservable = value;
                OnPropertyChanged(nameof(AccountObservable));
            }
        }

        // mendapatkan detail account
        public void GetAccountDetails()
        {
            AccountObservable.Id = App.Account.Id;
            AccountObservable.Name = App.Account.Name;
            AccountObservable.Username = App.Account.Username;
            AccountObservable.IncludeAdult = App.Account.IncludeAdult == true ? "Dewasa" : "Non Dewasa";
            AccountObservable.AvatarUrl = "https://www.gravatar.com/avatar/" + App.Account.Avatar.Gravatar.Hash;
        }
    }
}
