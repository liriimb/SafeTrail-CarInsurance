using CarInsuranceFinal.Data;
using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceFinal.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }
        
        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string userId)
        {
            // This will retrieve the user from the database by their ID
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(string userId)
        {
            return await _context.Cars.Where(c => c.ApplicationUserId == userId).ToListAsync();
        }
    }
}
