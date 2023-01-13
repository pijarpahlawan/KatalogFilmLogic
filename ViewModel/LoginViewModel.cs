using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDbLib.Client;

namespace KatalogFilm.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            IsLoginVisible = true;
            _loginCommand = new RelayCommand(async param => await Login(), null);
            RWJson.WriteToJson("api-key", "a39c8049ea4b22d58e5ed78f6f09e62b", "api.json");
        }

        private string _username = string.Empty;
        private string _password = string.Empty;
        private bool _isLoginVisible;
        private string _errorMessage = string.Empty;
        private ICommand _loginCommand;

        public bool IsLoginVisible
        {
            get => _isLoginVisible;
            set
            {
                _isLoginVisible = value;
                OnPropertyChanged(nameof(IsLoginVisible));
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
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand LoginCommand
        {
            get
            {
                _loginCommand ??= new RelayCommand(async param => await Login(), null);
                return _loginCommand;
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public async Task Login()
        {
            string apiKey = RWJson.ReadFromJSON("api-key", "api.json");
            //login
            try
            {
                using (var client = new TMDbClient(apiKey))
                {
                    var sessionLogin = await client.AuthenticationGetUserSessionAsync(_username, _password);
                    RWJson.WriteToJson("session-id", sessionLogin.SessionId, "session.json");
                    var mainWindow = new MainWindow();
                    mainWindow.DataContext = new MainViewModel();
                    IsLoginVisible = false;
                }
            }
            catch (Exception)
            {
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
