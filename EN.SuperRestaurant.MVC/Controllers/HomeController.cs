using AutoMapper;
using EN.SuperRestaurant.MVC.Data;
using EN.SuperRestaurant.MVC.Models;
using EN.SuperRestaurant.MVC.Models.Homes;
using EN.SuperRestaurant.MVC.Models.Meals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EN.SuperRestaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Data and Const

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var homePageVM = new HomePageViewModel();

            homePageVM.BestSoldMeals = await GetBestSoldMeals();
            homePageVM.OurCustomers = await GetOurCustomers();

            return View(homePageVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Private Methods

        private async Task<List<MealViewModel>> GetBestSoldMeals()
        {
            var bestSoldMeals = await _context
                                        .Meals
                                        .OrderByDescending(meals => meals.Price)
                                        .Take(6)
                                        .ToListAsync();

            var bestSoldMealVMs = _mapper.Map<List<MealViewModel>>(bestSoldMeals);

            return bestSoldMealVMs;
        }


        private async Task<List<string>> GetOurCustomers()
        {
            var customers = await _context
                                    .Customers
                                    .Select(customers => customers.FullName)
                                    .ToListAsync();

            return customers;
        }

        #endregion
    }
}
