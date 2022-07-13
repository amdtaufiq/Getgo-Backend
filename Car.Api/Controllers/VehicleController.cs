using AutoMapper;
using Car.Core.CustomEntities;
using Car.Core.DTOs;
using Car.Core.Entities;
using Car.Core.Filters;
using Car.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Car.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(
            IVehicleService vehicleService,
            IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllVehicle([FromQuery] VehicleFilter filters)
        {
            var vehicles = _vehicleService.GetAllVehicle(filters);
            var vehicleDtos = _mapper.Map<IEnumerable<VehicleResponse>>(vehicles);

            var response = new ApiResponse<IEnumerable<VehicleResponse>>(vehicleDtos)
            {
                Message = new Message
                {
                    Description = "List Data Vehicle"
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleByID(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);
            var vehicleDto = _mapper.Map<VehicleResponse>(vehicle);

            var response = new ApiResponse<VehicleResponse>(vehicleDto)
            {
                Message = new Message
                {
                    Description = "Detail Data Vehicle"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest request)
        {
            var vehicleMap = _mapper.Map<Vehicle>(request);
            var result = await _vehicleService.AddVehicle(vehicleMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Create Vehicle"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(Guid id, UpdateVehicleRequest request)
        {
            var vehicleMap = _mapper.Map<Vehicle>(request);
            var result = await _vehicleService.UpdateVehicle(id, vehicleMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Update Vehicle"

                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var result = await _vehicleService.DeleteVehicle(id);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Delete Vehicle"
                }
            };

            return Ok(response);
        }
    }
}
