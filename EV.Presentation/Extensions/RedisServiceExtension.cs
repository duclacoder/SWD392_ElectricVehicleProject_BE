using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.Services;
using StackExchange.Redis;

namespace EV.Presentation.Extensions
{
    public static class RedisServiceExtension
    {
        public static void RedisService(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Redis");

            services.AddSingleton<IConnectionMultiplexer>(
                sp => ConnectionMultiplexer.Connect(connection!));

        }
    
    }
}
