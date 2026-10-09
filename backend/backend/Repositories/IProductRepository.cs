using backend.Models.Domain;

namespace backend.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByEanCodeAsync(string eanCode);
    }
}
