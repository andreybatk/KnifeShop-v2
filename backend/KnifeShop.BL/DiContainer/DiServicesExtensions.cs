using KnifeShop.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KnifeShop.BL.DiContainer
{
    public static class DiServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<AuthenticatorService>();
            services.AddScoped<AccessTokenGeneratorService>();

            services.AddSingleton<RefreshTokenGeneratorService>();
            services.AddSingleton<TokenGeneratorService>();
        }
    }
}