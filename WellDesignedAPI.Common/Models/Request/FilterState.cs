namespace WellDesignedAPI.Common.Models.Request
{
    public class FilterState
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public string? RangeFrom { get; set; }
        public string? RangeTo { get; set; }
    }

}
