namespace KnifeShop.Contracts.User
{
    public class GetFavoriteKnifesPaginated
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
