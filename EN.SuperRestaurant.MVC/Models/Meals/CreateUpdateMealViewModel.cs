using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Meals
{
    public class CreateUpdateMealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        public List<int> IngredientIds { get; set; } = [];
    }
}
