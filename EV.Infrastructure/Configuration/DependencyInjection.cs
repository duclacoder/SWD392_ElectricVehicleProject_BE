using EV.Application.Helpers;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.Services;
using EV.Infrastructure.DBContext;
using EV.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EV.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfranstructureToApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            //Database connection string
            services.AddDbContext<Swd392Se1834G2T1Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );


            //Repository injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IUserPackagesServices, UserPackagesService>();
            services.AddScoped<IUserPostsService, UserPostService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IInspectionFeesRepository, InspectionFeesRepository>();
            services.AddScoped<IAuctionsFeeRepository, AuctionsFeeRepository>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IPostPackageService, PostPackagesService>();
            services.AddScoped<IPostPackageRepository, PostPackageRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IVnPayService, VnPayService>();

            //Service injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();  
            services.AddScoped<IModelStateCheck, ModelStateCheck>();
            services.AddScoped<IInspectionFeesService, InspectionFeesService>();
            services.AddScoped<IAuctionsFeeService, AuctionsFeeService>();

            return services;
        }
    }
}
