using CarInsuranceFinal.Models;
using CarInsuranceFinal.Models.Schema;
using CarInsuranceFinal.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarInsuranceFinal.Controllers
{
    [Authorize(Roles = "Agent")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Index()
        {
            var claims = await _reviewRepository.GetClaimsForReviewAsync();
            return View(claims);
        }

        public async Task<IActionResult> Review(int id)
        {
            var claim = await _reviewRepository.GetClaimByIdAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            var files = await _reviewRepository.GetClaimFilesAsync(id);

            var model = new ReviewViewModel
            {
                Claim = claim,
                Files = files
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            await _reviewRepository.UpdateClaimStatusAsync(id, ClaimStatus.ClaimSatus.Accepted);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Deny(int id)
        {
            await _reviewRepository.UpdateClaimStatusAsync(id, ClaimStatus.ClaimSatus.Denied);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewRepository.DeleteClaimAndFilesAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
