using EN.SuperRestaurant.MVC.Models.Ingredients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Meals
{
    public class MealDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string? ImageName { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = [];
    }
}
