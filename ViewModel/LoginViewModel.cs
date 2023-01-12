using KatalogFilm.View;
using KatalogFilm.ViewModel.Helper;
using System;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace KatalogFilm.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            _isLoginVisible = true;
            _errorMessage = string.Empty;
            _username = string.Empty;
            _password = string.Empty;
            _loginCommand = new ViewModelCommand(async param => await Login());
            RWJson.WriteToJson("api-key", "a39c8049ea4b22d58e5ed78f6f09e62b", "api.json");
        }

        private string _username;
        private string _password;
        private bool _isLoginVisible;
        private string _errorMessage;
        private ViewModelCommand _loginCommand;

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
        public ViewModelCommand LoginCommand
        {
            get
            {
                _loginCommand ??= new ViewModelCommand(async param => await Login());
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
