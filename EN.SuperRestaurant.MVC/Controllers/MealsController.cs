using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SuperRestaurant.Entities.Meals;
using EN.SuperRestaurant.MVC.Data;
using AutoMapper;
using EN.SuperRestaurant.MVC.Models.Meals;
using Microsoft.AspNetCore.Mvc.Rendering;
using EN.SuperRestaurant.Entities.Ingredients;

namespace EN.SuperRestaurant.MVC.Controllers
{
    public class MealsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var meals = await _context
                                .Meals
                                .ToListAsync();

            var mealVMs = _mapper.Map<List<MealViewModel>>(meals);

            return View(mealVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                                .Meals
                                .Include(meal => meal.Ingredients)
                                .Where(meal => meal.Id == id)
                                .SingleOrDefaultAsync();

            if (meal == null)
            {
                return NotFound();
            }

            var mealVM = _mapper.Map<MealDetailsViewModel>(meal);

            return View(mealVM);
        }

        public IActionResult Create()
        {
            var createUpdateMealVM = new CreateUpdateMealViewModel();
            createUpdateMealVM.IngredientLookup = new MultiSelectList(_context.Ingredients, "Id", "Name");

            return View(createUpdateMealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateMealViewModel createUpdateMealViewModel)
        {
            if (ModelState.IsValid)
            {
                // Transform createUpdateMealViewModel -> Meal
                var meal = _mapper.Map<Meal>(createUpdateMealViewModel);

                // Update Meal Ingredients
                await UpdateMealIngredients(meal, createUpdateMealViewModel.IngredientIds);

                // Update Meal Price
                meal.Price = await GetMealPrice(meal.Ingredients);

                _context.Add(meal);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateMealViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }

        private async Task UpdateMealIngredients(Meal meal, List<int> ingredientIds)
        {
            // Clear meal ingredients
            meal.Ingredients.Clear();

            // Get ingredients from DB using the ingredientIds
            var ingredients = await _context
                                        .Ingredients
                                        .Where(ingredient => ingredientIds.Contains(ingredient.Id))
                                        .ToListAsync();

            // Add ingredients to Meal.Ingredients
            meal.Ingredients.AddRange(ingredients);
        }

        private async Task<decimal> GetMealPrice(List<Ingredient> ingredients)
        {
            var mealPrice = ingredients.Sum(ingredient => ingredient.Price);
            var mealPriceWithProfit = mealPrice * 1.4m;

            return mealPriceWithProfit;
        }

        #endregion
    }
}
