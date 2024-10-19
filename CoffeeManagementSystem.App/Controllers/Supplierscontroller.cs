using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace CoffeeManagementSystem.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private IBaseRepository<Supplier> _supplierRepository;

        public SuppliersController(IBaseRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;

        }

        public async Task<IActionResult> Index()
        {
            var Suppliers = await _supplierRepository.GetAll();
            ViewBag.SuppliersCount = Suppliers.Count();
            return View(Suppliers);
        }


        public async Task<IActionResult> Details(int id)
        {
            var Supplier = await _supplierRepository.GetById(id);


            if (Supplier == null)
            {

                return View("_page404");
            }

            return View(Supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier Supplier)
        {
            await _supplierRepository.AddItem(Supplier);
            return RedirectToAction(nameof(Index));

        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var Supplier = await _supplierRepository.GetById(id);
            if (Supplier == null)
            {
                return View("_page404");
            }
            return View("Edit", Supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier Supplier)
        {
            try
            {
                await _supplierRepository.UpdateItem(Supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }

        }

        // GET: Suppliers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var category = await _supplierRepository.GetById(id);
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
                await _supplierRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
