using backend.Data;
using backend.Models.Domain;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly DbAPIContext _context;
        private readonly IOpenFoodFactsService _off;

        public MealRepository(DbAPIContext context, IOpenFoodFactsService off)
        {
            _context = context;
            _off = off;
        }

        public async Task<Day?> GetDayAsync(int userId, DateOnly date)
        {
            var day = await _context.Days.FirstOrDefaultAsync(x => x.Date == date && x.UserId == userId);
            return day;
        }

        public async Task<List<MealItemDto>> GetMealItemsAsync(int dayId)
        {
            var mealItems = await _context.MealItems
               .Where(x => x.DayId == dayId)
               .Select(x => new MealItemDto
               {
                   Id = x.Id,
                   DayId = x.DayId,
                   MealNumber = x.MealNumber,
                   Amount = x.Amount,
                   IsBought = x.isBought,
                   Product = new ProductResponseDto
                   {
                       Id = x.Product.Id,
                       EanCode = x.Product.EanCode,
                       Name = x.Product.Name,
                       Brand = x.Product.Brand,
                       CaloriesPer100 = x.Product.CaloriesPer100,
                       ProteinPer100 = x.Product.ProteinPer100,
                       FatPer100 = x.Product.FatPer100,
                       CarbsPer100 = x.Product.CarbsPer100,
                       ImageUrl = x.Product.ImageUrl,
                       Quantity = x.Product.Quantity,
                       Unit = x.Product.Unit
                   }
               })
               .ToListAsync();
            return mealItems;
        }

        public async Task<MealItem> AddMealItemAsync(int userId, CreateMealItemAndDayDto request, DateOnly date)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.EanCode == request.EanCode);

            if (product == null)
            {
                var offProduct = await _off.GetByEanAsync(request.EanCode);

                product = new Product
                {
                    EanCode = request.EanCode,
                    Name = request.Name
                        ?? offProduct?.ProductName
                        ?? "Nepoznat proizvod",

                    Brand = request.Brand
                        ?? offProduct?.Brands,

                    CaloriesPer100 =
                        offProduct?.Nutriments?.EnergyKcal100g ?? 0,

                    ProteinPer100 =
                        offProduct?.Nutriments?.Proteins100g ?? 0,

                    FatPer100 =
                        offProduct?.Nutriments?.Fat100g ?? 0,

                    CarbsPer100 =
                        offProduct?.Nutriments?.Carbohydrates100g ?? 0,

                    Quantity = request.Quantity,
                    Unit = request.Unit
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            var day = await _context.Days
                .FirstOrDefaultAsync(d =>
                    d.UserId == userId &&
                    d.Date == date);

            if (day == null)
            {
                day = new Day
                {
                    UserId = userId,
                    Date = date
                };

                await _context.Days.AddAsync(day);
                await _context.SaveChangesAsync();
            }

            var mealItem = new MealItem
            {
                DayId = day.Id,
                ProductId = product.Id,
                MealNumber = request.MealNumber,
                Amount = request.Amount,
                isBought = false
            };

            await _context.MealItems.AddAsync(mealItem);
            await _context.SaveChangesAsync();

            return mealItem;
        }

        public async Task<bool> DeleteMealItemAsync(int mealItemId, int userId)
        {
            var mealItem = await _context.MealItems
                .Include(x => x.Day)
                .FirstOrDefaultAsync(x =>
                    x.Id == mealItemId &&
                    x.Day.UserId == userId);

            if (mealItem == null)
                return false;

            _context.MealItems.Remove(mealItem);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMealItemAmountAsync(int mealItemId, int userId, int amount)
        {
            var mealItem = await _context.MealItems
                .Include(x => x.Day)
                .FirstOrDefaultAsync(x =>
                    x.Id == mealItemId &&
                    x.Day.UserId == userId);

            if (mealItem == null)
                return false;

            mealItem.Amount = amount;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
