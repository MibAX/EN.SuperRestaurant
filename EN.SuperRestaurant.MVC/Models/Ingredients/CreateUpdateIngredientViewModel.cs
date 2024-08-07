using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Ingredients
{
    public class CreateUpdateIngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }
    }
}
