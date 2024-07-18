using FoodMenu.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenu.Data
{
    public class FoodMenuContext : DbContext
    {
        public FoodMenuContext(DbContextOptions<FoodMenuContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId
            });
            modelBuilder.Entity<DishIngredient>()
                .HasOne(d => d.Dish)
                .WithMany(di => di.DishIngridients)
                .HasForeignKey(d => d.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(i => i.Ingredient)
                .WithMany(di => di.DishIngredients)
                .HasForeignKey(i => i.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Cheese pizza", Price = 200.0, ImageUrl = "https://img.freepik.com/free-photo/delicious-pizza-traditional-italian-pizza_1328-3992.jpg?w=360&t=st=1721299732~exp=1721300332~hmac=11169019ed2c74071a26e18ce10164b01eca24c087a1b8bffd7e759d59f91afc" });
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Mozzarella" },
                new Ingredient { Id = 2, Name = "Tomato sauce" },
                new Ingredient { Id = 3, Name = "Parmesan" }
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1 },
                new DishIngredient { DishId = 1, IngredientId = 2 },
                new DishIngredient { DishId = 1, IngredientId = 3 }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}
