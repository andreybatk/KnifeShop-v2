namespace KnifeShop.API.Contracts.Google
{
    public class GoogleTokenPayload
    {
        public string Email { get; set; } = string.Empty;
        public string Aud { get; set; } = string.Empty;
    }
}
