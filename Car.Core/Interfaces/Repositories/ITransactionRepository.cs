using Car.Core.Entities;

namespace Car.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionByCarID(Guid id);
    }
}
