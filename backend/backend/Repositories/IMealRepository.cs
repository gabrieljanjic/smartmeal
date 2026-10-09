using backend.Models.Domain;
using backend.Models.DTOs;

namespace backend.Repositories
{
    public interface IMealRepository
    {
        Task<Day?> GetDayAsync(int userId, DateOnly date);
        Task<List<MealItemDto>> GetMealItemsAsync(int dayId);
        Task<MealItem> AddMealItemAsync(int userId, CreateMealItemAndDayDto request, DateOnly date);
        Task<bool> DeleteMealItemAsync(int mealItemId, int userId);
        Task<bool> UpdateMealItemAmountAsync(int mealItemId, int userId, int amount);
    }
}
