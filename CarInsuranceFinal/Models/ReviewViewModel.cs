using CarInsuranceFinal.Models.Schema;

namespace CarInsuranceFinal.Models
{
    public class ReviewViewModel
    {
        public Claim? Claim { get; set; } // Contains all details about the claim
        public IEnumerable<FileCreation>? Files { get; set; } // Contains all files associated with the claim
    }
}
