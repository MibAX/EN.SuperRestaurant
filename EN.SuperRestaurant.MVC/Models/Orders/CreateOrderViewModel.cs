using EN.SuperRestaurant.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EN.SuperRestaurant.MVC.Models.Orders
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }
        public string? Notes { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Meals")]

        [Required]
        public List<int> MealIds { get; set; } = [];

        [Required]
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        // ######### Lookups

        [ValidateNever]
        public SelectList CustomerLookup { get; set; }

        [ValidateNever]
        public MultiSelectList MealLookup { get; set; }
    }
}
