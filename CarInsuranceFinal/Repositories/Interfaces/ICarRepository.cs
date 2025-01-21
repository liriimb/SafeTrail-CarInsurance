using CarInsuranceFinal.Models.Schema;

namespace CarInsuranceFinal.Repositories.Interfaces
{
    public interface ICarRepository
    {
            Task<IEnumerable<Car>> GetAllCarsAsync();
            Task<Car> GetCarByIdAsync(int id);
            Task AddCarAsync(Car car);
            Task UpdateCarAsync(Car car);
            Task DeleteCarAsync(int id);
            Task<ApplicationUser> GetApplicationUserByIdAsync(string userId);
            Task<IEnumerable<Car>> GetCarsByUserIdAsync(string userId);
    }
}
