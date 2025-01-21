using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarInsuranceFinal.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cars = await _carRepository.GetAllCarsAsync();
            var userCars = cars.Where(c => c.ApplicationUserId == userId).ToList();
            var user = await _carRepository.GetApplicationUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            //car count check
            ViewBag.HasReachedCarLimit = !user.MultiCar && userCars.Count >= 1;

            return View(userCars);
        }
        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _carRepository.AddCarAsync(car);

                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Details/5/Delete
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carRepository.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            //null check
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCar = await _carRepository.GetCarByIdAsync(id);
                    if (existingCar == null)
                    {
                        return NotFound();
                    }
                    existingCar.Model = car.Model;
                    existingCar.Year = car.Year;
                    existingCar.RegistrationNumber = car.RegistrationNumber;

                    await _carRepository.UpdateCarAsync(existingCar);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _carRepository.GetCarByIdAsync(car.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }
        // POST: Car/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid car ID");
            }

            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            await _carRepository.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
