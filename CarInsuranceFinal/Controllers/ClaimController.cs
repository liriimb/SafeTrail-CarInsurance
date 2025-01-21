using CarInsuranceFinal.Data;
using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Models;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceFinal.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimController(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        // GET: Claim/Index
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var claims = await _claimRepository.GetClaimsByUserIdAsync(userId);
            return View(claims);
        }

        // GET: Create
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cars = await _claimRepository.GetCarsByUserIdAsync(userId);

            if (cars == null || !cars.Any())
            {
                ViewBag.Cars = new List<Car>();
                Debug.WriteLine("No cars found for user.");
            }
            else
            {
                ViewBag.Cars = cars;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Schema.Claim claim, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                claim.Status = ClaimStatus.ClaimSatus.Sent;
                claim.ClaimDate = DateTime.Now;
                claim.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _claimRepository.AddClaimAsync(claim);

                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    var fileCreation = new FileCreation
                    {
                        ClaimId = claim.Id,
                    };

                    //file save
                    await SaveFileDataAsync(uploadedFile, fileCreation, _claimRepository);
                }

                return RedirectToAction(nameof(Index));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cars = await _claimRepository.GetCarsByUserIdAsync(userId);
            ViewBag.Cars = cars ?? new List<Car>();

            return View(claim);
        }

        //Files
        private async Task SaveFileDataAsync(IFormFile file, FileCreation fileCreation, IClaimRepository claimRepository)
        {
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            if (file.Length > 50 * 1024 * 1024)
            {
                throw new InvalidOperationException("File size exceeds the 50 MB limit.");
            }

            var fileCount = Directory.GetFiles(uploadsDirectory).Length + 1;
            var fileName = $"{fileCount}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            fileCreation.FileName = fileName;
            fileCreation.FileType = file.ContentType;
            fileCreation.FileExtension = Path.GetExtension(fileName);

            await claimRepository.AddFileCreationAsync(fileCreation);
        }

        // DELETE: Claim/Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _claimRepository.DeleteClaimAndFilesAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
