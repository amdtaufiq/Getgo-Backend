using Car.Core.Interfaces.Repositories;

namespace Car.Core.Interfaces.Unit
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleRepository VehicleRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync(bool delete = false);
    }
}
