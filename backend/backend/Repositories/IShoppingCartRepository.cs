using backend.Models.Domain;
using backend.Models.DTOs;

namespace backend.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Product> AddToShoppingCartAsync(int userId, CreateProductAndAddToShoppingCartDto request);
        Task<List<ShoppingCart>> GetMyShoppingCartProductsAsync(int userId);
        Task<ShoppingCart?> UpdateBoughtAtAsync(int id, int userId);
        Task<ShoppingCart?> UpdateAmountAsync(int id, int userId, int amount);
        Task<bool> DeleteShoppingItemAsync(int id, int userId);
    }
}