using EN.SuperRestaurant.MVC.Models.Orders;
using EN.SuperRestaurant.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace EN.SuperRestaurant.MVC.Models.Customers
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
        
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public List<OrderViewModel> Orders { get; set; } = [];
    }
}
