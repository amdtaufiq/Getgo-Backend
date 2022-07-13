using Car.Core.Entities;
using Car.Core.Interfaces.Repositories;
using Car.Infrastructure.Data;

namespace Car.Infrastructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {

        public VehicleRepository(CarDbContext context) : base(context)
        {

        }

        public Vehicle GetVehicleName(string name)
        {
            return _ctx.Vehicles
                .Where(x => x.IsDelete == false && x.VehicleName == name)
                .FirstOrDefault();
        }

        public Vehicle GetVehicleById(Guid id)
        {
            return _ctx.Vehicles
                .Where(x => x.IsDelete == false && x.Id == id)
                .FirstOrDefault();
        }
    }
}
