using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICijenePriceService _cijenePriceService;

        public ProductsController(
            IProductRepository productRepository,
            ICijenePriceService cijenePriceService)
        {
            _productRepository = productRepository;
            _cijenePriceService = cijenePriceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySearchCijeneDev(
            [FromQuery] string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Ok(Array.Empty<object>());

            var result =
                await _cijenePriceService.SearchProductsAsync(query);

            if (!result.Success)
                return StatusCode(
                    result.StatusCode,
                    result.Body);

            return Content(result.Body,"application/json");
        }

        [HttpGet("{eanCode}")]
        public async Task<IActionResult> GetProductByEanCode(string eanCode)
        {
            var product = await _productRepository.GetByEanCodeAsync(eanCode);

            return Ok(product);
        }
    }
}