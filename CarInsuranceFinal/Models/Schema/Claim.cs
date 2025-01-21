using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarInsuranceFinal.Models.Schema
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string? AccidentDescription { get; set; }
        public string? OtherPersonFullName { get; set; }
        public string? OtherPersonRegistrationNumber { get; set; }
        // Foreign key relationship to ApplicationUser (UserId is a string)
        public string? ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser? ApplicationUser { get; set; } // many to one
        // Foreign key relationship to Car (CarId is an int)
        public int? CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; } // many to one
        public ICollection<FileCreation>? FileCreations { get; set; }
        // New property for claim status
        public ClaimStatus.ClaimSatus Status { get; set; } = ClaimStatus.ClaimSatus.Sent; // Default status is sent
    }
}
