using EN.SuperRestaurant.MVC.Models.Meals;

namespace EN.SuperRestaurant.MVC.Models.Homes
{
    public class HomePageViewModel
    {
        public List<string> OurCustomers { get; set; } = [];
        public List<MealViewModel> BestSoldMeals { get; set; } = [];

    }
}
