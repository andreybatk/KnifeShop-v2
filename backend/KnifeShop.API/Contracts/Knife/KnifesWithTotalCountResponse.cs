namespace KnifeShop.API.Contracts.Knife
{
    public class KnifesWithTotalCountResponse
    {
        public List<GetKnifesResponse>? Knifes { get; set; }
        public int TotalCount { get; set; }
    }
}
