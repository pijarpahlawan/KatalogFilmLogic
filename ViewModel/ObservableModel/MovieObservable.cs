using System.Windows.Media;

namespace KatalogFilm.ViewModel.ObservableModel
{
    public class MovieObservable : ViewModelBase
    {
        private bool _adult;
        private int _id;
        private string _originalLanguage = string.Empty;
        private string _originalTitle = string.Empty;
        private string _overview = string.Empty;
        private string _posterPath = string.Empty;
        private ImageBrush? _poster;
        public bool Adult
        {
            get => _adult;
            set
            {
                _adult = value;
                OnPropertyChanged(nameof(Adult));
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
        public string OriginalLanguage
        {
            get => _originalLanguage;
            set
            {
                _originalLanguage = value;
                OnPropertyChanged(nameof(OriginalLanguage));
            }
        }
        public string OriginalTitle
        {
            get => _originalTitle;
            set
            {
                _originalTitle = value;
                OnPropertyChanged(nameof(OriginalTitle));
            }
        }
        public string Overview
        {
            get => _overview;
            set
            {
                _overview = value;
                OnPropertyChanged(nameof(Overview));
            }
        }
        public string PosterPath
        {
            get => _posterPath;
            set
            {
                _posterPath = value;
                OnPropertyChanged(nameof(PosterPath));
            }
        }
        public ImageBrush? Poster
        {
            get => _poster;
            set
            {
                _poster = value;
                OnPropertyChanged(nameof(Poster));
            }
        }
    }
}
