using backend.Models.DTOs;

namespace backend.Services
{
    public interface IOpenFoodFactsService
    {
        Task<OpenFoodFactsProduct?> GetByEanAsync(string eanCode);
        Task<OpenFoodFactsProduct?> SearchByNameAsync(string? name);
    }

}
