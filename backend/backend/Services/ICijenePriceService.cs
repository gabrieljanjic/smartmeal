using backend.Models.DTOs;

namespace backend.Services
{
    public interface ICijenePriceService
    {
        Task<List<CijeneChain>> GetPricesAsync(string eanCode);
        Task<(bool Success, int StatusCode, string Body)> SearchProductsAsync(string query);
    }
}
