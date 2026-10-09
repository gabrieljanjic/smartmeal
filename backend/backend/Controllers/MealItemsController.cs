using backend.Models.DTOs;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/meal-items")]
    [ApiController]
    public class MealItemsController : ControllerBase
    {

        private readonly IMealRepository _mealRepository;

        public MealItemsController(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllMeals([FromQuery] DateOnly date)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            var day = await _mealRepository.GetDayAsync(userId, date);

            if (day == null)
                return Ok(new { items = Array.Empty<MealItemResponseDto>() });

            var mealItems = await _mealRepository.GetMealItemsAsync(day.Id);

            return Ok(new { items = mealItems });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMealItem([FromBody] CreateMealItemAndDayDto request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            if (string.IsNullOrWhiteSpace(request.Date))
                return BadRequest("Datum je obavezan");

            if (string.IsNullOrWhiteSpace(request.EanCode))
                return BadRequest("EAN kod je obavezan");

            if (request.MealNumber < 1)
                return BadRequest("Broj obroka mora biti najmanje 1");

            if (request.Amount < 1)
                return BadRequest("Količina mora biti najmanje 1");

            if (!DateOnly.TryParse(request.Date, out var date))
                return BadRequest("Neispravan datum");

            var mealItem = await _mealRepository.AddMealItemAsync(
                userId,
                request,
                date);

            return Ok(new
            {
                mealItem.Id,
                mealItem.DayId,
                mealItem.ProductId,
                mealItem.MealNumber,
                mealItem.Amount,
                mealItem.isBought
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMealItem(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized();

            var deleted = await _mealRepository.DeleteMealItemAsync(id, userId);

            if (!deleted)
                return NotFound();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] int amount)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized();

            if (amount < 1)
                return BadRequest("Količina mora biti najmanje 1");

            var updated = await _mealRepository.UpdateMealItemAmountAsync(id, userId, amount);

            if (!updated)
                return NotFound();

            return Ok();
        }
    }
}