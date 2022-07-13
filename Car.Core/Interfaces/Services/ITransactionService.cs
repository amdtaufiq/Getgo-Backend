using Car.Core.DTOs;
using Car.Core.Entities;

namespace Car.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<bool> AddTransaction(CreateTransactionRequest transaction);
        Task<bool> UpdateTransaction(Guid id, Transaction transaction);
        IEnumerable<Transaction> GetAllTransaction();
    }
}
