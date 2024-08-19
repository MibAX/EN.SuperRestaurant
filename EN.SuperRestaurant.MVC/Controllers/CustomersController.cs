using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EN.SuperRestaurant.Entities.Customers;
using EN.SuperRestaurant.MVC.Data;
using AutoMapper;
using EN.SuperRestaurant.MVC.Models.Customers;

namespace EN.SuperRestaurant.MVC.Controllers
{
    public class CustomersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _context
                                    .Customers
                                    .ToListAsync();

            var customerVMs = _mapper.Map<List<CustomerViewModel>>(customers);

            return View(customerVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                    .Customers
                                    .Include(customer => customer.Orders)
                                    .Where(customer => customer.Id == id)
                                    .SingleOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var customerDetailsVM = _mapper.Map<CustomerDetailsViewModel>(customer);

            return View(customerDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCustomerViewModel createUpdateCustomerViewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(createUpdateCustomerViewModel);

                _context.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateCustomerViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                    .Customers
                                    .FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var createUpdateCustomerViewModel = _mapper.Map<CreateUpdateCustomerViewModel>(customer);

            return View(createUpdateCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCustomerViewModel createUpdateCustomerViewModel)
        {
            if (id != createUpdateCustomerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO 
                // 1- Get the customer from DB using the id
                var customer = await _context
                                        .Customers
                                        .FindAsync(id);

                // 2- Check for nullability
                if (customer == null)
                {
                    return NotFound();
                }

                // 3- Patch(copy) createUpdateCustomerViewModel -> customer
                _mapper.Map(createUpdateCustomerViewModel, customer);


                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(createUpdateCustomerViewModel.Id))
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

            return View(createUpdateCustomerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Functions

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        } 

        #endregion
    }
}
