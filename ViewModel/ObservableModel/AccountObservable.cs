using TMDbLib.Objects.Account;

namespace KatalogFilm.ViewModel.ObservableModel
{
    public class AccountObservable : ViewModelBase
    {
        private Avatar? _avatar;
        private int _id;
        private string _name = string.Empty;
        private string _username = string.Empty;
        private string _includeAdult = string.Empty;

        public Avatar? Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnPropertyChanged(nameof(Avatar));
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string IncludeAdult
        {
            get => _includeAdult;
            set
            {
                _includeAdult = value;
                OnPropertyChanged(nameof(IncludeAdult));
            }
        }
    }
}
