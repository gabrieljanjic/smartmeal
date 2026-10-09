using backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DbAPIContext :DbContext
    {
        public DbAPIContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    }
}

