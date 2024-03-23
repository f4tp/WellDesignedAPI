
using System.ComponentModel.DataAnnotations;

namespace WellDesignedAPI.Application.DTOs.Entity.GetMovie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public DateTime? DateReleased { get; set; }
        [Required]
        public string MovieTitle { get; set; }
        public string? Synopsis { get; set; }
        public decimal? PopularityLevel { get; set; }
        public int? NoOfVotes { get; set; }
        public decimal? VoteAverageScore { get; set; }
        [Required]
        public string MainLanguage { get; set; }
        public string? UrlForPoster { get; set; }
        public IEnumerable<MovieGenreDto>? MovieGenres { get; set; }
    }
}
