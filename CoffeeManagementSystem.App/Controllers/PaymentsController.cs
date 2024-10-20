using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace CoffeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentsController : Controller
    {
        private IBaseRepository<Payment> _PaymentRepository;

        public PaymentsController(IBaseRepository<Payment> PaymentRepository)
        {
            _PaymentRepository = PaymentRepository;

        }

        public async Task<IActionResult> Index()
        {
            var Payments = await _PaymentRepository.GetAll();
            ViewBag.PaymentsCount = Payments.Count();
            return View(Payments);
        }


        public async Task<IActionResult> Details(int id)
        {
            var Payment = await _PaymentRepository.GetById(id);


            if (Payment == null)
            {

                return View("_page404");
            }

            return View(Payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment Payment)
        {
            await _PaymentRepository.AddItem(Payment);
            return RedirectToAction(nameof(Index));

        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var Payment = await _PaymentRepository.GetById(id);
            if (Payment == null)
            {
                return View("_page404");
            }
            return View("Edit", Payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Payment Payment)
        {
            try
            {
                await _PaymentRepository.UpdateItem(Payment);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }

        }

        // GET: Payments/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var category = await _PaymentRepository.GetById(id);
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
                await _PaymentRepository.DeleteItem(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
