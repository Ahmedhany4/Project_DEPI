using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace CoffeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private IBaseRepository<Customer> _customerRepository;
        private readonly MyDbContext _context;

        public CustomersController(IBaseRepository<Customer> customerRepository, MyDbContext context)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerRepository.GetAll();
            ViewBag.CustomersCount = customers.Count();
            return View(customers);
        }

     
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _customerRepository.GetById(id);
              

            if (customer == null)
            {

                return View("_page404");
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
                await _customerRepository.AddItem(customer);
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return View("_page404");
            }
            return View("Edit",customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            try
            {
                await _customerRepository.UpdateItem(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }

        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var category = await _customerRepository.GetById(id);
            if (category == null)
            {
                Response.StatusCode = 404;
                return View("_Page404");
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _customerRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
