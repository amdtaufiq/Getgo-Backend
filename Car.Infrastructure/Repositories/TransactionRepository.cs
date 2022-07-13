using Car.Core.Entities;
using Car.Core.Interfaces.Repositories;
using Car.Infrastructure.Data;

namespace Car.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(CarDbContext context) : base(context)
        {

        }


        public IEnumerable<Transaction> GetTransactionByCarID(Guid id)
        {
            return _ctx.Transactions
               .Where(x => x.IsDelete == false && x.CarId == id)
               .AsEnumerable();
        }
    }
}
