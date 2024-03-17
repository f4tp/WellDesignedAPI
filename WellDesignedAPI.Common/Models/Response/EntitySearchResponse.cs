
namespace WellDesignedAPI.Common.Models.Response
{
    public class EntitySearchResponse
    {
        public IEnumerable<object> Records { get; set; }
        public int RecordTotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
