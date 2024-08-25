using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Meals
{
    public class MealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string? ImageName { get; set; }
    }
}
