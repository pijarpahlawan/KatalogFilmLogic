using System.Collections.Generic;
using System.Windows.Media;

namespace KatalogFilm.Model
{
    public class Movie
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public List<int> GenreIds { get; set; }
        public int Id { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public ImageBrush Poster { get; set; }
    }
}
