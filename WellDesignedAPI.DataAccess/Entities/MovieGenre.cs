using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellDesignedAPI.DataAccess.Entities
{
    [Table("MovieGenres")]
    public class MovieGenre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MovieGenreId", TypeName = "INTEGER")]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }

        [Required]
        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }


        private MovieGenre(){} // EF requires parameterless constructor

        public MovieGenre(Movie movie, Genre genre)
        {
            if (movie == null)
                throw new ArgumentException("Object creation requires a movie");
            if (genre == null)
                throw new ArgumentException("Object creation requires a movie");
            if(movie.Id < 1)
                throw new ArgumentException("Object creation requires a movie id");
            if (genre.Id < 1)
                throw new ArgumentException("Object creation requires a genre id");

            MovieId = movie.Id;
            GenreId = genre.Id;
        }


    }
}
