using Car.Core.Entities;
using Car.Core.Exceptions;
using Car.Core.Filters;
using Car.Core.Interfaces.Services;
using Car.Core.Interfaces.Unit;
using Microsoft.Extensions.Logging;

namespace Car.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public VehicleService(
            IUnitOfWork unit,
            ILoggerFactory loggerFactory)
        {
            _unit = unit;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger("Vehicle");
        }

        public async Task<bool> AddVehicle(Vehicle vehicle)
        {
            try
            {
                await _unit.VehicleRepository.Add(vehicle);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Vehicle Add => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> DeleteVehicle(Guid id)
        {
            try
            {
                var data = await _unit.VehicleRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Vehicle doesn't exist!");
                }
                _unit.VehicleRepository.Delete(data);
                await _unit.SaveChangesAsync(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Vehicle Delete => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public IEnumerable<Vehicle> GetAllVehicle(VehicleFilter filters)
        {
            try
            {
                var datas = _unit.VehicleRepository.GetAll();
                ApplyNearbyLocation(ref datas, filters);
                ApplySearchName(ref datas, filters.Name);

                return datas;
            }
            catch (Exception e)
            {
                _logger.LogError("Vehicle List => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        private void ApplySearchName(ref IEnumerable<Vehicle> datas, string name)
        {
            if(name == null)
            {
                return;
            }

            datas = datas.Where(x => x.VehicleName.ToLower().Contains(name.ToLower())).ToList();
        }

        private void ApplyNearbyLocation(ref IEnumerable<Vehicle> datas, VehicleFilter filters)
        {
            if (filters.LocationX == null || filters.LocationY == null) 
            {
                return;
            }

            datas = datas.OrderBy(x => ((int.Parse(filters.LocationX) - x.LocationX) + (int.Parse(filters.LocationY) - x.LocationY))).ToList();
        }

        public async Task<Vehicle> GetVehicleById(Guid id)
        {
            try
            {
                var data = await _unit.VehicleRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Vehicle doesn't exist!");
                }
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError("Vehicle By ID => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }

        public async Task<bool> UpdateVehicle(Guid id, Vehicle vehicle)
        {
            try
            {
                var data = await _unit.VehicleRepository.GetById(id);
                if (data == null)
                {
                    throw new NotFoundException("Vehicle doesn't exist!");
                }

                //Set value
                data.LocationX = vehicle.LocationX;
                data.LocationY = vehicle.LocationY;

                _unit.VehicleRepository.Update(data);
                await _unit.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Vehicle Update => " + e.Message);
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
