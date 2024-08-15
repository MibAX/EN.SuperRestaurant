using EN.SuperRestaurant.Entities.Customers;
using EN.SuperRestaurant.Entities.Meals;
using System.ComponentModel.DataAnnotations.Schema;

namespace EN.SuperRestaurant.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Notes { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Meal> Meals { get; set; } = [];
    }
}
