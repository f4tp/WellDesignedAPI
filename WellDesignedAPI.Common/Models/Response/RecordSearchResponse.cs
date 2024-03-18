
namespace WellDesignedAPI.Common.Models.Response
{
    public class RecordSearchResponse
    {
        public IEnumerable<object>? Records { get; set; }
        public int? RecordTotalCount { get; set; }
        public int? CurrentPage { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
