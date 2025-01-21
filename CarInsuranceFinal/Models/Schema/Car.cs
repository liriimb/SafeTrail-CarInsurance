using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarInsuranceFinal.Models.Schema
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string? Model { get; set; }
        [MaxLength(4)]
        [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Year must be a 4-digit number.")]
        public string? Year { get; set; } // Changed to string

        public string? RegistrationNumber { get; set; }

        // Foreign key relationship to ApplicationUser (UserId is a string)
        public string? ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; } // one to many

        // Car to Claims relationship (CarId is an int)
        public ICollection<Claim>? Claims { get; set; } // one to many
    }
}
