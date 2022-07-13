using Car.Core.Enumerations;

namespace Car.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid CarId { get; set; }
        public bool IsBooked { get; set; }
        public StatusVehicle StatusVehicle { get; set; }
    }
}
