using System.Windows;
using TMDbLib.Client;
using TMDbLib.Objects.Account;

namespace KatalogFilm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TMDbClient Client = new TMDbClient("a39c8049ea4b22d58e5ed78f6f09e62b");
        public static AccountDetails? Account = null;
        internal const string poppinFontPath = @"Font\Poppins-Regular.ttf";
        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }
    }
}
