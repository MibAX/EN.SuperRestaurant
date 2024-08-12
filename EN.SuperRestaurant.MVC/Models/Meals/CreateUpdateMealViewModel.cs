using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Meals
{
    public class CreateUpdateMealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Ingredients")]
        public List<int> IngredientIds { get; set; } = [];


        // =========== Lookups

        [ValidateNever]
        public MultiSelectList IngredientLookup { get; set; }
    }
}
