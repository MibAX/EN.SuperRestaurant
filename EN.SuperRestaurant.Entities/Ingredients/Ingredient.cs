using EN.SuperRestaurant.Entities.Meals;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.Entities.Ingredients
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        public List<Meal> Meals { get; set; } = [];
    }
}
