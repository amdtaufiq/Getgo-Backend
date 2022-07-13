using Car.Core.DTOs;
using Car.Core.Interfaces.Repositories;
using FluentValidation;

namespace Car.Infrastructure.Validations.Transaction
{
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionRequest>
    {
        private readonly ITransactionRepository _transaction;
        public UpdateTransactionValidator(ITransactionRepository transaction)
        {
            _transaction = transaction;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Must(AvailableTrasaction).WithMessage("{PropertyName} not found");

            RuleFor(x => x.StatusVehicle)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }

        public bool AvailableTrasaction(Guid id)
        {
            return _transaction.GetById(id) != null;
        }


    }
}
