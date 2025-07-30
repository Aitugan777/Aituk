using HaveServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HaveServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ASeller> Sellers { get; set; }
        public DbSet<AShop> Shops { get; set; }
        public DbSet<AProduct> Products { get; set; }
        public DbSet<ACategory> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Указываем точность и масштаб для свойства 'Cost' в AProduct
            modelBuilder.Entity<AProduct>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)"); // Явно указываем тип данных и точность

            // Альтернативное использование HasPrecision для задания точности и масштаба
            modelBuilder.Entity<AProduct>()
                .Property(p => p.Cost)
                .HasPrecision(18, 2); // Устанавливаем точность 18 и масштаб 2
        }
    }
}
