namespace WellDesignedAPI.Common.Models.Request
{
    public class FilterState
    {
        public string PropertyName { get; set; }
        public string? ValueFromOrEqualTo { get; set; }
        public string? ValueTo { get; set; }
    }

}
