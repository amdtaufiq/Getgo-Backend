using Car.Core.Entities;
using Car.Core.Interfaces.Repositories;
using Car.Core.Interfaces.Unit;
using Car.Infrastructure.Data;
using Car.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Car.Infrastructure.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarDbContext _ctx;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITransactionRepository _transactionRepository;

        public UnitOfWork(CarDbContext ctx)
        {
            _ctx = ctx;
        }

        public IVehicleRepository VehicleRepository => _vehicleRepository ?? new VehicleRepository(_ctx);
        public ITransactionRepository TransactionRepository => _transactionRepository ?? new TransactionRepository(_ctx);

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (((BaseEntity)entityEntry.Entity).IsDelete == true)
                {
                    ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = true;
                }
                else
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }
            }

            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync(bool delete = false)
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            var username = "System";
            //if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            //{
            //    ClaimsIdentity claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            //    var claims = claimsIdentity.Claims.Select(x => new { type = x.Type, value = x.Value });
            //    if (claims.Any())
            //    {
            //        username = claims.SingleOrDefault(x => x.type == "Email").value;
            //    }
            //    else
            //    {
            //        username = "System";
            //    }
            //}

            foreach (var entityEntry in entries)
            {
                if (delete == true)
                {
                    ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).DeletedBy = username;
                    ((BaseEntity)entityEntry.Entity).IsDelete = true;
                }
                else
                {
                    if (((BaseEntity)entityEntry.Entity).IsDelete == true)
                    {
                        ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                        ((BaseEntity)entityEntry.Entity).DeletedBy = username;
                        ((BaseEntity)entityEntry.Entity).IsDelete = true;
                    }
                    else
                    {
                        ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                        ((BaseEntity)entityEntry.Entity).UpdatedBy = username;
                        ((BaseEntity)entityEntry.Entity).IsDelete = false;
                    }
                }
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).CreatedBy = username;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
