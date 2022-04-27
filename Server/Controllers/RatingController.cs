using GolfCourseMVC.Server.Services.RatingService;
using GolfCourseMVC.Shared.Models.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return null;

            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            _ratingService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var ratings = await _ratingService.GetAllRatingsAysnc();
            return Ok(ratings);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();
            return Ok(rating);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var ratings = await _ratingService.GetRatingsByCourse(courseId);

            if (ratings == null) return NotFound();

            return Ok(ratings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            if (model == null) return BadRequest();

            bool wasSuccess = await _ratingService.CreateRatingAsync(model);
            if (wasSuccess) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, RatingEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid) return BadRequest();

            bool wasSuccess = await _ratingService.UpdateRatingAsync(model);
            if (wasSuccess) return Ok();
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();

            bool wasSuccess = await _ratingService.DeleteRatingAsync(id);
            if (wasSuccess) return Ok();
            else return BadRequest();
        }

    }
}
