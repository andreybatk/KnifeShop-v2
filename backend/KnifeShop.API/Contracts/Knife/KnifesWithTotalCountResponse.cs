using KnifeShop.DB.Contracts;

namespace KnifeShop.API.Contracts.Knife
{
    public class KnifesWithTotalCountResponse
    {
        public List<GetKnifesResponse> Knifes { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
