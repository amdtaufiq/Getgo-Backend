using System.ComponentModel.DataAnnotations;

namespace Car.Core.Enumerations
{
    public enum StatusVehicle
    {
        [Display(Name = "To Pick Up")]
        PickUp = 0,
        [Display(Name = "Pick Up Point")]
        PickUpPoint = 1,
        [Display(Name = "To Destination")]
        Destination = 2,
        [Display(Name = "Arrive")]
        Arrive = 3,
        [Display(Name = "Complete")]
        Complete = 4
    }
}
