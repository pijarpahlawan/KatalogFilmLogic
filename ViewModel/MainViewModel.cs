using System.Windows.Input;

namespace KatalogFilm.ViewModel
{
    // view model untuk main
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(object loginViewModel)
        {
            HomeCommand = new RelayCommand(Home, null);
            MyFavoriteCommand = new RelayCommand(MyFavorite, null);
            AccountCommand = new RelayCommand(Account, null);
            LoginCommand = new RelayCommand(Login, null);

            //start page
            CurrentView = new HomeViewModel();
            IsMainVisible = false;

            _currentLoginViewModel = loginViewModel as LoginViewModel;
        }

        private readonly LoginViewModel _currentLoginViewModel;
        private object _isMainVisible;
        //private object _isLoginVisible;
        private object _currentView;
        private ICommand _logoutCommand;

        // page saat ini
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }
        // menentukan visibilitas main window
        public object IsMainVisible
        {
            get => _isMainVisible;
            set
            {
                _isMainVisible = value;
                OnPropertyChanged(nameof(IsMainVisible));
            }
        }
        // menentukan visibilitas login window
        //public object IsLoginVisible
        //{
        //    get => _isLoginVisible;
        //    set
        //    {
        //        _isLoginVisible = value;
        //        OnPropertyChanged(nameof(IsLoginVisible));
        //    }
        //}

        // perintah di navbar
        public ICommand HomeCommand { get; set; }
        public ICommand MyFavoriteCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand
        {
            get
            {
                _logoutCommand ??= new RelayCommand(param => Logout(), null);
                return _logoutCommand;
            }
        }

        // fungsi untuk perpindahan page di navbar
        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void MyFavorite(object obj) => CurrentView = new MyFavoritesViewModel();
        private void Account(object obj) => CurrentView = new AccountViewModel();
        private void Login(object obj) => CurrentView = new LoginViewModel();

        public void Logout()
        {
            App.Client.Dispose();
            IsMainVisible = false;
            _currentLoginViewModel.IsLoginVisible = true;
        }
    }
}
