namespace Car.Core.DTOs
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public bool IsBooked { get; set; }
        public string StatusVehicle { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTransactionRequest
    {
        public Guid CarId { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }

    public class UpdateTransactionRequest
    {
        public Guid Id { get; set; }
        public string StatusVehicle { get; set; }
    }
}
