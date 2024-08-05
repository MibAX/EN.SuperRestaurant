using EN.SuperRestaurant.Entities.Customers;
using EN.SuperRestaurant.Entities.Ingredients;
using EN.SuperRestaurant.Entities.Meals;
using EN.SuperRestaurant.Entities.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EN.SuperRestaurant.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
