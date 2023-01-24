using KatalogFilm.ViewModel.ObservableModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using TMDbLib.Objects.Movies;

namespace KatalogFilm.ViewModel.Helper
{
    internal static class Converter
    {
        internal static MovieObservable ToMovieObservable(this Movie movie)
        {
            const string endpoint = "https://image.tmdb.org/t/p/original";
            return new MovieObservable()
            {
                Adult = movie.Adult,
                Id = movie.Id,
                OriginalLanguage = movie.OriginalLanguage,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                PosterPath = endpoint + movie.PosterPath,
                Poster = PathToImageBrush(endpoint + movie.PosterPath),
                Genres = new ObservableCollection<string>(movie.Genres.Select(x => x.Name)),
                ReleaseDate = movie.ReleaseDate,
            };
        }
        public static ImageBrush PathToImageBrush(string imagePath)
        {
            ImageBrush result;
            BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
            result = new ImageBrush(bitmap);
            return result;
        }
    }
}
