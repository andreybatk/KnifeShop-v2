using System.ComponentModel.DataAnnotations;

namespace KnifeShop.Contracts.Auth
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}