using CarInsuranceFinal.Data;
using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceFinal.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetClaimsForReviewAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == ClaimStatus.ClaimSatus.Sent || c.Status == ClaimStatus.ClaimSatus.InReview)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Car) 
                .ToListAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(int claimId)
        {
            return await _context.Claims
                .Include(c => c.ApplicationUser)  
                .Include(c => c.Car)              
                .FirstOrDefaultAsync(c => c.Id == claimId);
        }

        public async Task UpdateClaimStatusAsync(int claimId, ClaimStatus.ClaimSatus newStatus)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = newStatus;
                _context.Claims.Update(claim);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FileCreation>> GetClaimFilesAsync(int claimId)
        {
            return await _context.Files
                .Where(f => f.ClaimId == claimId)
                .ToListAsync();
        }

        public async Task DeleteClaimAndFilesAsync(int claimId)
        {
            var claim = await _context.Claims
                .Include(c => c.FileCreations) 
                .FirstOrDefaultAsync(c => c.Id == claimId);

            if (claim != null)
            {
                if (claim.FileCreations != null && claim.FileCreations.Any())
                {
                    foreach (var file in claim.FileCreations)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        _context.Files.Remove(file);
                    }
                }

                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }
    }
}
