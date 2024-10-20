using CoffeeManagementSystem.Entities.Models;
using CoffeeManagementSystem.Repositories.Interfaces;
using ContextFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CoffeeManagementSystem.App.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoriesController : Controller
    {

        // GET: CategoriesController
        private IBaseRepository<Category> _categoryRepository;
        public CategoriesController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }


        public async Task<IActionResult> Index()
        {
            
            var categories = await _categoryRepository.GetAll();
            ViewBag.categoriescount = categories.Count();
            return View(categories);
        }


        public async Task<ActionResult> Create()
        {        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category item)

        { 
            await _categoryRepository.AddItem(item);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Details(int id)
        {
            var product = await _categoryRepository.GetById(id);
            return View(product);
        }

        public async Task<ActionResult> Edit(int id)
        {
           Category category = await _categoryRepository.GetById(id);

            // Check if the product is null (not found)
            if (category == null)
            {
                Response.StatusCode = 404;
                return View("_Page404");
            }
            return View("Edit", category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category item)
        {
            try
            {   
                await _categoryRepository.UpdateItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit"); 
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
    
            var category = await _categoryRepository.GetById(id);
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
                await _categoryRepository.DeleteItem(id); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

