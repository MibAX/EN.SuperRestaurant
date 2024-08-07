using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SuperRestaurant.Entities.Ingredients;
using EN.SuperRestaurant.MVC.Data;
using AutoMapper;
using EN.SuperRestaurant.MVC.Models.Ingredients;

namespace EN.SuperRestaurant.MVC.Controllers
{
    public class IngredientsController : Controller
    {
        #region Data & Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngredientsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ingredients = await _context
                                        .Ingredients
                                        .ToListAsync();

            var ingredientVMs = _mapper.Map<List<IngredientViewModel>>(ingredients);

            return View(ingredientVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context
                                        .Ingredients
                                        .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientVM = _mapper.Map<IngredientDetailsViewModel>(ingredient);

            return View(ingredientVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateIngredientViewModel createUpdateIngredientVM)
        {
            if (ModelState.IsValid)
            {
                var ingredient = _mapper.Map<Ingredient>(createUpdateIngredientVM);

                _context.Add(ingredient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateIngredientVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context
                                    .Ingredients
                                    .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var createUpdateIngredientVM = _mapper.Map<CreateUpdateIngredientViewModel>(ingredient);

            return View(createUpdateIngredientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateIngredientViewModel createUpdateIngredientVM)
        {
            if (id != createUpdateIngredientVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ingredient = _mapper.Map<Ingredient>(createUpdateIngredientVM);

                try
                {
                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(createUpdateIngredientVM.Id))
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

            return View(createUpdateIngredientVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context
                                    .Ingredients
                                    .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }

        #endregion
    }
}
