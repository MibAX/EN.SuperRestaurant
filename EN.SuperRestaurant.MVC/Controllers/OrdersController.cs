using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EN.SuperRestaurant.Entities.Orders;
using EN.SuperRestaurant.MVC.Data;
using AutoMapper;
using EN.SuperRestaurant.MVC.Models.Orders;
using EN.SuperRestaurant.Entities.Meals;

namespace EN.SuperRestaurant.MVC.Controllers
{
    public class OrdersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _context
                                .Orders
                                .Include(order => order.Customer)
                                .ToListAsync();

            var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);

            return View(orderVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateOrderVM = new CreateOrderViewModel();

            createUpdateOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.MealLookup = new MultiSelectList(_context.Meals, "Id", "Name");

            return View(createUpdateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel createOrderVM)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(createOrderVM);

                order.OrderTime = DateTime.Now;

                await UpdateOrderMeals(order, createOrderVM.MealIds);

                order.TotalPrice = GetOrderTotalPrice(order.Meals);

                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            createOrderVM.MealLookup = new MultiSelectList(_context.Meals, "Id", "Name");

            return View(createOrderVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context
                                    .Orders
                                    .Include(order => order.Meals)
                                    .Where(order => order.Id == id)
                                    .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var updateOrderVM = _mapper.Map<UpdateOrderViewModel>(order);

            updateOrderVM.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            updateOrderVM.MealLookup = new MultiSelectList(_context.Meals, "Id", "Name");

            return View(updateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateOrderViewModel updateViewModel)
        {
            if (id != updateViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get the order including meals from the DB
                var order = await _context
                                    .Orders
                                    .Include(order => order.Meals)
                                    .Where(order => order.Id == id)
                                    .SingleOrDefaultAsync();

                if (order == null)
                {
                    return NotFound();
                }

                // Patch the order
                _mapper.Map(updateViewModel, order);

                // Update order meals
                await UpdateOrderMeals(order, updateViewModel.MealIds);

                // Update order total price
                order.TotalPrice = GetOrderTotalPrice(order.Meals);

                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(updateViewModel.Id))
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


            updateViewModel.CustomerLookup = new SelectList(_context.Customers, "Id", "FullName");
            updateViewModel.MealLookup = new MultiSelectList(_context.Meals, "Id", "Name");

            return View(updateViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private async Task UpdateOrderMeals(Order order, List<int> mealIds)
        {
            // Clear Order.Meal
            order.Meals.Clear();

            // Load Meals from MealIds
            var meals = await _context
                                .Meals
                                .Where(meal => mealIds.Contains(meal.Id))
                                .ToListAsync();

            // Add Meals to Order
            order.Meals.AddRange(meals);
        }

        private decimal GetOrderTotalPrice(List<Meal> meals)
        {
            var mealsPrice = meals.Sum(meal => meal.Price);
            var mealsPriceWithVAT = mealsPrice * 1.16m;

            return mealsPriceWithVAT;
        }

        #endregion

    }
}
