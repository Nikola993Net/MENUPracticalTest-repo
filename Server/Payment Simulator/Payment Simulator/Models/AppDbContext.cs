using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Payment_Simulator.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
			
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Store>().HasData(
                new Store() { Id = 1, StoreAccountBalance = 50000, LastTransactionId = 15}
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer() { Id =1 , Username = "Marko93", CustomerAccountBalance = 20000, StoreId = 1 },
                new Customer() { Id =2,  Username = "Petar88", CustomerAccountBalance = 30000, StoreId = 1 },
                new Customer() {Id =3, Username = "luka22", CustomerAccountBalance = 40000, StoreId = 1 }
            );

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
