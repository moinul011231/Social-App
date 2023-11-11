using Microsoft.EntityFrameworkCore;
using SocialAPI.TData;
using SocialAPI.TInterfaces;
using SocialAPI.TServices;

namespace SocialAPI.TExtensions
{
    public static class ApplicatonServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config) 
        {
            services.AddCors();
            services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddEndpointsApiExplorer();

            services.AddDbContext<TDataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("LearnSocialApp"));
            });

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
