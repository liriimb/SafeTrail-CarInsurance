using CarInsuranceFinal.Data;
using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceFinal.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(int id)
        {
            return await _context.Claims.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsByUserIdAsync(string userId)
        {
            return await _context.Claims
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();
        }

        public async Task AddClaimAsync(Claim claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClaimAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClaimAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Claim>> GetClaimsByCarIdAsync(int carId)
        {
            return await _context.Claims
                .Where(c => c.CarId == carId)
                .ToListAsync();
        }
        public async Task UpdateClaimStatusAsync(int claimId, ClaimStatus.ClaimSatus status)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim != null)
            {
                claim.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClaimAndFilesAsync(int claimId)
        {
            var claim = await _context.Claims
                .Include(c => c.FileCreations) 
                .FirstOrDefaultAsync(c => c.Id == claimId);

            if (claim != null)
            {
                // Delete associated files
                foreach (var file in claim.FileCreations)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(string userId)
        {
            return await _context.Cars
                .Where(car => car.ApplicationUserId == userId)
                .ToListAsync();
        }
        public async Task AddFileCreationAsync(FileCreation fileCreation)
        {
            await _context.Files.AddAsync(fileCreation);
            await _context.SaveChangesAsync();
        }
    }
}
