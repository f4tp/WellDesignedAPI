using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WellDesignedAPI.DataAccess.Entities
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

        public IEnumerable<MovieGenre> MovieGenres { get; set; }

    }
}
