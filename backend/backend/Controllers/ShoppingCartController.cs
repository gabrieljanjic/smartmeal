using backend.Repositories;
using backend.Services;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICijenePriceService _prices;

        public ShoppingCartController(
            IShoppingCartRepository shoppingCartRepository,
            ICijenePriceService prices)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _prices = prices;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(
            [FromBody] CreateProductAndAddToShoppingCartDto request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            if (request.Amount < 1)
                return BadRequest("Količina mora biti najmanje 1.");

            var product = await _shoppingCartRepository.AddToShoppingCartAsync(userId, request);

            return Ok(product);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyShoppingCartProducts()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            var items = await _shoppingCartRepository.GetMyShoppingCartProductsAsync(userId);

            var result = new List<object>();

            foreach (var item in items)
            {
                var prices =
                    await _prices.GetPricesAsync(
                        item.Product.EanCode);

                result.Add(new
                {
                    id = item.Id,
                    amount = item.Amount,
                    boughtAt = item.BoughtAt,

                    product = item.Product,

                    prices = prices.Select(x => new
                    {
                        chain = x.Chain,
                        avgPrice = x.AvgPrice,
                        minPrice = x.MinPrice,
                        maxPrice = x.MaxPrice,
                        unit = x.Unit,
                        quantity = x.Quantity
                    }).ToList()
                });
            }

            return Ok(result);
        }

        [HttpPut("{id}/bought-at")]
        [Authorize]
        public async Task<IActionResult> UpdateBoughtAt(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            var shoppingCartItem =await _shoppingCartRepository.UpdateBoughtAtAsync(id, userId);

            if (shoppingCartItem == null)
                return NotFound("Stavka u košarici nije pronađena.");

            return Ok(shoppingCartItem);
        }

        [HttpPut("{id}/amount")]
        [Authorize]
        public async Task<IActionResult> UpdateAmount(
            int id,
            [FromBody] UpdateAmountDto request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            if (request.Amount < 1)
                return BadRequest("Količina mora biti najmanje 1.");

            var shoppingCartItem =
                await _shoppingCartRepository.UpdateAmountAsync(id, userId, request.Amount);

            if (shoppingCartItem == null)
                return NotFound("Stavka u košarici nije pronađena.");

            return Ok(shoppingCartItem);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdString, out var userId))
                return Unauthorized("Niste prijavljeni.");

            var deleted = await _shoppingCartRepository.DeleteShoppingItemAsync(id, userId);

            if (!deleted)
                return NotFound("Stavka u košarici nije pronađena.");

            return Ok();
        }
    }
}