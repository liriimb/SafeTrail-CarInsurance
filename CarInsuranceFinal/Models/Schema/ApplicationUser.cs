using Microsoft.AspNetCore.Identity;

namespace CarInsuranceFinal.Models.Schema
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool MultiCar { get; set; }
        //relationships
        public ICollection<Car>? Cars { get; set; } //one to many
        public ICollection<Claim>? Claims { get; set; } //one to many
    }
}
