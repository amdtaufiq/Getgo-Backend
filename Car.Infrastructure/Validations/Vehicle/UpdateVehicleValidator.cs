using Car.Core.DTOs;
using Car.Core.Interfaces.Repositories;
using FluentValidation;

namespace Car.Infrastructure.Validations.Vehicle
{
    public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleRequest>
    {
        private readonly IVehicleRepository _vehicle;
        public UpdateVehicleValidator(IVehicleRepository vehicle)
        {
            _vehicle = vehicle;

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .Must(AvailableCarId).WithMessage("{PropertyName} not found");

            RuleFor(x => x.LocationX)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} of trainings must be greater than 0.");

            RuleFor(x => x.LocationY)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} of trainings must be greater than 0.");
        }

        public bool AvailableCarId(Guid id)
        {
            return _vehicle.GetVehicleById(id) != null;
        }
    }
}
