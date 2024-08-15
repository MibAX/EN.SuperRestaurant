using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.MVC.Models.Orders
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }
    }
}
