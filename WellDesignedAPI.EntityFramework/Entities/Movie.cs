using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WellDesignedAPI.EntityFramework.Entities
{

    [Table("Movies")]
    public class Movie
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MovieId", TypeName = "INTEGER")]
        public int Id { get; set; } //different names to underlying db schema as these are expected from the searching / filtering state so doesn't expose schema

        [Column("ReleaseDate", TypeName = "DATE")]
        public DateTime? DateReleased { get; set; }
        
        [Required]
        [Column("Title", TypeName = "NVARCHAR(500)")]
        public string MovieTitle { get; private set; }
        
        
        [Column("Overview", TypeName = "NVARCHAR(MAX)")]
        public string? Synopsis { get; set; }

        [Column("Popularity", TypeName = "DECIMAL(10, 3)")]
        public decimal? PopularityLevel { get; set; }

        [Column("VoteCount", TypeName = "INTEGER")]
        public int? NoOfVotes { get; set; }

        [Column("VoteAverage", TypeName = "DECIMAL(3, 1)")]
        public decimal? VoteAverageScore { get; set; }
        
        [Required]
        [Column("OriginalLanguage", TypeName = "NVARCHAR(10)")]
        public string? MainLanguage { get; private set; }


        [Column("PosterUrl", TypeName = "NVARCHAR(MAX)")]
        public string? UrlForPoster { get; set; }


        //as using EF, to minimise risk with accidental deletion / update, backing prop has to be interacted with through the methods below
        private List<MovieGenre>? _movieGenres;
        public IEnumerable<MovieGenre>? MovieGenres { get { return _movieGenres; } }


        private Movie(){ } //EF requires a parameterless constructor

        //Constructor to only allow creation of object with correct props and 
        public Movie(string movieTitle, string mainLanguage)
        {
            if(string.IsNullOrWhiteSpace(movieTitle))
                throw new ArgumentException("Object creation requires a movie title");

            if (string.IsNullOrWhiteSpace(mainLanguage))
                throw new ArgumentException("Object creation requires a main language");

            //To do - validate language passed in is valid, will lookup in valid instances
            _movieGenres = new List<MovieGenre>();
            MovieTitle = movieTitle;
            MainLanguage = mainLanguage;
        }

        public MovieGenre AddMovieGenre(Genre genre)
        {
            var movieGenreToAdd = new MovieGenre(this, genre);
            _movieGenres.Add(movieGenreToAdd);
            return movieGenreToAdd;
        }

        public void RemoveMovieGenre(MovieGenre movieGenre)
        {
            _movieGenres.Remove(movieGenre);
        }
    }
}
