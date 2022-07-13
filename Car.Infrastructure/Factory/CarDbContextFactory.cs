using Car.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Car.Infrastructure.Factory
{
    public class CarDbContextFactory : IDesignTimeDbContextFactory<CarDbContext>
    {
        public CarDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CarDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            builder.UseNpgsql(connectionString);
            return new CarDbContext(builder.Options);
        }
    }
}
