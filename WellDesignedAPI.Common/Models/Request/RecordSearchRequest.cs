using System.ComponentModel.DataAnnotations;

namespace WellDesignedAPI.Common.Models.Request
{
    public class RecordSearchRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Search requires a valid page number")]
        public int RequiredPageNumber { get; set; }
        [Required]
        [Range(1, 200, ErrorMessage = "Search returns a maximum of 200 results")]
        public int NumberOfResultsPerPage { get; set; }
        public IEnumerable<FilterState>? Filters { get; set; }
        public string? SortBy { get; set; }
        public bool? SortAscending { get; set; }
        public string? SearchTerm { get; set; }

    }
}
