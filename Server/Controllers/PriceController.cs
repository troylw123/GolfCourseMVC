using GolfCourseMVC.Server.Services.PriceService;
using GolfCourseMVC.Shared.Models.Price;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
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

            _priceService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var prices = await _priceService.GetAllPricesAysnc();
            return Ok(prices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var price = await _priceService.GetPriceByIdAsync(id);

            if (price == null) return NotFound();

            return Ok(price);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var prices = await _priceService.GetPricesByCourse(courseId);

            if (prices == null) return NotFound();

            return Ok(prices);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PriceCreate model)
        {
            if (model == null) return BadRequest();

            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccess = await _priceService.CreatePriceAsync(model);

            if (wasSuccess) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, PriceEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid) return BadRequest();

            if (model.Id != id) return BadRequest();

            bool wasSuccess = await _priceService.UpdatePriceAsync(model);
            if (!wasSuccess) return BadRequest();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var price = await _priceService.GetPriceByIdAsync(id);

            if (price == null) return NotFound();

            bool wasSuccess = await _priceService.DeletePriceAsync(id);

            if (!wasSuccess) return BadRequest();
            return Ok();
        }
    }
}
