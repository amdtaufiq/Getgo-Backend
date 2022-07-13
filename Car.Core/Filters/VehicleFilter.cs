using System.ComponentModel.DataAnnotations;

namespace Car.Core.Filters
{
    public class VehicleFilter
    {
        [Required]
        public string LocationX { get; set; }
        [Required]
        public string LocationY { get; set; }

        public string? Name { get; set; }
    }
}
