using KatalogFilm.View;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDbLib.Objects.Authentication;

namespace KatalogFilm.ViewModel
{
    // view model untuk login window
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            _isLoginVisible = true;
            _loginCommand = new RelayCommand(async param => await Login());
            _mainWindow = new MainWindow();
            _mainViewModel = new MainViewModel(this);
            _mainWindow.DataContext = _mainViewModel;
        }

        private readonly MainWindow _mainWindow;
        private readonly MainViewModel _mainViewModel;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private object _isLoginVisible;
        //private object _isMainVisible;
        private string _errorMessage = string.Empty;
        private ICommand _loginCommand;

        // property yang menentukan visibilitas login window
        public object IsLoginVisible
        {
            get => _isLoginVisible;
            set
            {
                _isLoginVisible = value;
                OnPropertyChanged(nameof(IsLoginVisible));
            }
        }
        // property yang menentukan visibilitas main window
        //public object IsMainVisible
        //{
        //    get => _isMainVisible;
        //    set
        //    {
        //        _isMainVisible = value;
        //        OnPropertyChanged(nameof(IsMainVisible));
        //    }
        //}
        // menampung inputan username
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        // menampung inputan password
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        // perintah untuk tombol login
        public ICommand LoginCommand
        {
            get
            {
                _loginCommand ??= new RelayCommand(async param => await Login());
                return _loginCommand;
            }
        }
        // menampilkan pesan error ketika username dan password salah
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        // fungsi untuk melakukan login
        public async Task Login()
        {
            try
            {
                // mendapatkan sesi login
                var sessionLogin = await App.Client.AuthenticationGetUserSessionAsync(_username, _password);
                // memasukkan informasi id sesi ke client
                await App.Client.SetSessionInformationAsync(sessionLogin.SessionId, SessionType.UserSession);
                // mendapatkan account saat ini
                App.Account = App.Client.ActiveAccount;
                IsLoginVisible = false;
                _mainViewModel.IsMainVisible = true;
            }
            catch (Exception)
            {
                // jika terdapat kesalahan login, maka akan memunculkan error
                ErrorMessage = "Gagal login. Pastikan username dan password benar";
                IsLoginVisible = true;
            }
            finally
            {
                Username = string.Empty;
                Password = string.Empty;
            }
        }
    }
}
