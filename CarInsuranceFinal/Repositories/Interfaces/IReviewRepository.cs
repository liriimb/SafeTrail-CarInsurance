using CarInsuranceFinal.Models.Schema;

namespace CarInsuranceFinal.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Claim>> GetClaimsForReviewAsync();
        Task<Claim> GetClaimByIdAsync(int claimId);
        Task UpdateClaimStatusAsync(int claimId, ClaimStatus.ClaimSatus newStatus);
        Task<IEnumerable<FileCreation>> GetClaimFilesAsync(int claimId);
        Task DeleteClaimAndFilesAsync(int claimId);
    }
}
