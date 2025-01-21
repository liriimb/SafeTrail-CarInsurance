using CarInsuranceFinal.Models.Schema;

namespace CarInsuranceFinal.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetAllClaimsAsync();
        Task<Claim> GetClaimByIdAsync(int id);
        Task<IEnumerable<Claim>> GetClaimsByUserIdAsync(string userId);
        Task AddClaimAsync(Claim claim);
        Task UpdateClaimAsync(Claim claim);
        Task DeleteClaimAsync(int id);
        Task<IEnumerable<Claim>> GetClaimsByCarIdAsync(int carId);
        Task UpdateClaimStatusAsync(int claimId, ClaimStatus.ClaimSatus status);
        Task DeleteClaimAndFilesAsync(int claimId);
        Task<IEnumerable<Car>> GetCarsByUserIdAsync(string userId);
        Task AddFileCreationAsync(FileCreation fileCreation);
    }
}
