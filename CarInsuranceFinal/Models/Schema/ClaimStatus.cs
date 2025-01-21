namespace CarInsuranceFinal.Models.Schema
{
    public class ClaimStatus
    {
        public enum ClaimSatus
        {
            Sent,       // Initial status when the claim is created        
            InReview,   // When someone is reviewing the claim       
            Accepted,   // Claim has been approved         
            Denied      // Claim has been rejected
        }
    }
}
