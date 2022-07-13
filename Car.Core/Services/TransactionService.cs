using Car.Core.DTOs;
using Car.Core.Entities;
using Car.Core.Exceptions;
using Car.Core.Interfaces.Services;
using Car.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;

namespace Car.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public TransactionService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Transaction");
        }

        public async Task<bool> AddTransaction(CreateTransactionRequest request)
        {
            var transactions = _unit.TransactionRepository.GetTransactionByCarID(request.CarId);
            var countData = transactions.Where(x => x.IsBooked).Count();
            if ( countData != 0)
            {
                throw new BadRequestException("Car not available");
            }
            var vehicle = await _unit.VehicleRepository.GetById(request.CarId);
            var result = (request.LocationX - vehicle.LocationX) + (request.LocationY - vehicle.LocationY);
            if(result != 2)
            {
                throw new BadRequestException("Car out of reach");
            }
            try
            {
                await _unit.TransactionRepository.Add(new Transaction
                {
                    CarId = request.CarId,
                    IsBooked = true,
                    StatusVehicle = Enumerations.StatusVehicle.PickUp
                });
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Transaction Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }


        public IEnumerable<Transaction> GetAllTransaction()
        {
            try
            {
                var datas = _unit.TransactionRepository.GetAll();

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("Transaction List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        
        public async Task<bool> UpdateTransaction(Guid id, Transaction transaction)
        {
            try
            {
                var data = await _unit.TransactionRepository.GetById(id);

                if (data == null)
                {
                    throw new NotFoundException("Transaction doesn't exist!");
                }

                if (!data.IsBooked)
                {
                    throw new BadRequestException("Transaction completed!");
                }

                //Set value
                if (transaction.StatusVehicle == Enumerations.StatusVehicle.Complete)
                    data.IsBooked = false;
                data.StatusVehicle = transaction.StatusVehicle;

                _unit.TransactionRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Transaction Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
