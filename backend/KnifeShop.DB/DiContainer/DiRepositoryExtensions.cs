using KnifeShop.DB.Repositories.Knifes;
using KnifeShop.DB.Repositories.Token;
using KnifeShop.DB.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace KnifeShop.DB.DiContainer
{
    public static class DiRepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IKnifeRepository, KnifeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}