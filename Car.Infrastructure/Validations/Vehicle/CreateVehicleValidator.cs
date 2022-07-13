using Car.Core.DTOs;
using Car.Core.Interfaces.Repositories;
using FluentValidation;

namespace Car.Infrastructure.Validations.Vehicle
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleRequest>
    {
        private readonly IVehicleRepository _vehicle;
        public CreateVehicleValidator(IVehicleRepository vehicle)
        {
            _vehicle = vehicle;

            RuleFor(x => x.VehicleName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Must(UniqueCarName).WithMessage("{PropertyName} is already");

            RuleFor(x => x.LocationX)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} of trainings must be greater than 0.");

            RuleFor(x => x.LocationY)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} of trainings must be greater than 0.");
        }

        public bool UniqueCarName(string vehicleName)
        {
            return _vehicle.GetVehicleName(vehicleName) == null;
        }
    }
}
