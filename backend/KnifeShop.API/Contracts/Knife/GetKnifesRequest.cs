namespace KnifeShop.API.Contracts.Knife
{
    public class GetKnifesRequest
    {
        public string? Search { get; set; }
        public string? SortItem { get; set; }
        public string? SortOrder { get; set; }
    }
}