using Car.Core.Entities;

namespace Car.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Vehicle GetVehicleName(string name);
        Vehicle GetVehicleById(Guid id);
    }
}
