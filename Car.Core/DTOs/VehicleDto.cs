namespace Car.Core.DTOs
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }
        public string? VehicleName { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateVehicleRequest
    {
        public string? VehicleName { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }

    public class UpdateVehicleRequest
    {
        public Guid Id { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }
}
