namespace Car.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public string? VehicleName { get; set; } 
        public int LocationX { get; set; }    
        public int LocationY { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
