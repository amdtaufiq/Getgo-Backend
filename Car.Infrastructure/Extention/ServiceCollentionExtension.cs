using Car.Core.Interfaces.Repositories;
using Car.Core.Interfaces.Services;
using Car.Core.Interfaces.Unit;
using Car.Core.Services;
using Car.Infrastructure.Data;
using Car.Infrastructure.Repositories;
using Car.Infrastructure.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Car.Infrastructure.Extention
{
    public static class ServiceCollentionExtension
    {
        public static void AddDbContexts(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            services.AddDbContext<CarDbContext>(opt =>
                opt.UseNpgsql(connectionString),
                ServiceLifetime.Transient);
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Repository
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            //Service
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<ITransactionService, TransactionService>();

            //Unit
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Service API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
