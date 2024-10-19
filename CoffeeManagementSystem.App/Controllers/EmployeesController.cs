using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace CoffeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private IBaseRepository<Employee> _employeeRepository;
        private readonly MyDbContext _context;

        public EmployeesController(IBaseRepository<Employee> employeeRepository, MyDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Employees = await _employeeRepository.GetAll();
            ViewBag.EmployeesCount = Employees.Count();
            return View(Employees);
        }


        public async Task<IActionResult> Details(int id)
        {
            var Employee = await _employeeRepository.GetById(id);


            if (Employee == null)
            {

                return View("_page404");
            }

            return View(Employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee Employee)
        {
            await _employeeRepository.AddItem(Employee);
            return RedirectToAction(nameof(Index));

        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var Employee = await _employeeRepository.GetById(id);
            if (Employee == null)
            {
                return View("_page404");
            }
            return View("Edit", Employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee Employee)
        {
            try
            {
                await _employeeRepository.UpdateItem(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }

        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var category = await _employeeRepository.GetById(id);
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
                await _employeeRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
