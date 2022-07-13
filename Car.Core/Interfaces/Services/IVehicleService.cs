using Car.Core.Entities;
using Car.Core.Filters;

namespace Car.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetAllVehicle(VehicleFilter filters);
        Task<Vehicle> GetVehicleById(Guid id);
        Task<bool> AddVehicle(Vehicle vehicle);
        Task<bool> UpdateVehicle(Guid id, Vehicle vehicle);
        Task<bool> DeleteVehicle(Guid id);
    }
}
