using backend.Data;
using backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbAPIContext _context;

        public ProductRepository(DbAPIContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByEanCodeAsync(string eanCode)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.EanCode == eanCode);
        }
    }
}