using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Domain.DomainEntities
{
    public class MovieDomEnt
    {

        public int Id { get; set; }
        public DateTime? DateReleased { get; set; }
        public string MovieTitle { get; private set; }
        public string? Synopsis { get; set; }
        public decimal? PopularityLevel { get; set; }
        public int? NoOfVotes { get; set; }

        public decimal? VoteAverageScore { get; set; }

        public string? MainLanguage { get; private set; }


        public string? UrlForPoster { get; set; }


        //as using EF, to minimise risk with accidental deletion / update, backing prop has to be interacted with through the methods below
        private List<MovieGenreDomEnt>? _movieGenres;
        public IEnumerable<MovieGenreDomEnt>? MovieGenres { get { return _movieGenres; } }


        //private MovieDomEnt() { } //EF requires a parameterless constructor

        //Constructor to only allow creation of object with correct props and 
        public MovieDomEnt(string movieTitle, string mainLanguage)
        {
            if (string.IsNullOrWhiteSpace(movieTitle))
                throw new ArgumentException("Object creation requires a movie title");

            if (string.IsNullOrWhiteSpace(mainLanguage))
                throw new ArgumentException("Object creation requires a main language");

            //To do - validate language passed in is valid, will lookup in valid instances
            _movieGenres = new List<MovieGenreDomEnt>();
            MovieTitle = movieTitle;
            MainLanguage = mainLanguage;
        }

        public MovieGenreDomEnt AddMovieGenre(GenreDomEnt genre)
        {
            var movieGenreToAdd = new MovieGenreDomEnt(this, genre);
            _movieGenres.Add(movieGenreToAdd);
            return movieGenreToAdd;
        }

        public void RemoveMovieGenre(MovieGenreDomEnt movieGenre)
        {
            _movieGenres.Remove(movieGenre);
        }
    }
}
