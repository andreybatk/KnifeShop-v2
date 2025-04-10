using System.ComponentModel.DataAnnotations;

namespace KnifeShop.API.Contracts.Auth
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}