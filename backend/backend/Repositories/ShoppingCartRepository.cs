using backend.Data;
using backend.Models.Domain;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DbAPIContext _context;
        private readonly IOpenFoodFactsService _off;

        public ShoppingCartRepository(
            DbAPIContext context,
            IOpenFoodFactsService off)
        {
            _context = context;
            _off = off;
        }

        public async Task<Product> AddToShoppingCartAsync(
            int userId,
            CreateProductAndAddToShoppingCartDto request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.EanCode == request.EanCode);

            if (product == null)
            {
                var offProduct = await _off.GetByEanAsync(request.EanCode) ?? await _off.SearchByNameAsync(request.Name);

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

                    ImageUrl = offProduct?.ImageUrl,
                    Quantity = request.Quantity,
                    Unit = request.Unit
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            var shoppingCartItem = new ShoppingCart
            {
                UserId = userId,
                ProductId = product.Id,
                Amount = request.Amount
            };

            await _context.ShoppingCarts.AddAsync(shoppingCartItem);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<ShoppingCart>>
            GetMyShoppingCartProductsAsync(int userId)
        {
            return await _context.ShoppingCarts.Where(x => x.UserId == userId).Include(x => x.Product).ToListAsync();
        }

        public async Task<ShoppingCart?> UpdateBoughtAtAsync(int id,int userId)
        {
            var shoppingCartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (shoppingCartItem == null)
                return null;

            shoppingCartItem.BoughtAt =shoppingCartItem.BoughtAt != null ? null : DateTime.Now;

            await _context.SaveChangesAsync();

            return shoppingCartItem;
        }

        public async Task<ShoppingCart?> UpdateAmountAsync(int id, int userId, int amount)
        {
            var shoppingCartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == id & x.UserId == userId);

            if (shoppingCartItem == null)
                return null;

            shoppingCartItem.Amount = amount;

            await _context.SaveChangesAsync();

            return shoppingCartItem;
        }

        public async Task<bool> DeleteShoppingItemAsync(int id, int userId)
        {
            var shoppingCartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (shoppingCartItem == null)
                return false;

            _context.ShoppingCarts.Remove(shoppingCartItem);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}