using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Entities;

namespace TechStoreMVC.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
