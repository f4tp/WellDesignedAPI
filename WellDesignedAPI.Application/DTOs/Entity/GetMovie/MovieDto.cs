
namespace WellDesignedAPI.Application.DTOs.Entity.GetMovie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public DateTime? DateReleased { get; set; }
        public string MovieTitle { get; set; }
        public string? Synopsis { get; set; }
        public decimal? PopularityLevel { get; set; }
        public int? NoOfVotes { get; set; }
        public double? VoteAverageScore { get; set; }
        public string MainLanguage { get; set; }
        public string? UrlForPoster { get; set; }
        public IEnumerable<MovieGenreDto>? MovieGenres { get; set; }
    }
}
