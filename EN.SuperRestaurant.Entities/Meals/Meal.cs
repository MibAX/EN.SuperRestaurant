using EN.SuperRestaurant.Entities.Ingredients;
using EN.SuperRestaurant.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.Entities.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")] 
        public decimal Price { get; set; }

        public string? ImageName { get; set; }


        public List<Ingredient> Ingredients { get; set; } = [];

        public List<Order> Orders { get; set; } = [];
    }
}
