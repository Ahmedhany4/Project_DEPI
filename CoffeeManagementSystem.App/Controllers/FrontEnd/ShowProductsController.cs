using CoffeeManagementSystem.Entities.Models;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.App.Controllers.FrontEnd
{
    public class ShowsController : Controller
    {
        private IBaseRepository<Product> _productRepository;
        private IBaseRepository<Category> _categoryRepository;
        private IBaseRepository<Supplier> _supplierRepository;


        public ShowsController(IBaseRepository<Product> productRepository, IBaseRepository<Category> categoryRepository, IBaseRepository<Supplier> supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }
        [Route("Show/products")]
        public async Task<IActionResult> Products()
        {
            var categories = await _categoryRepository.GetAll();
            var suppliers = await _supplierRepository.GetAll();
            var products = await _productRepository.GetAll();

            foreach (var product in products)
            {
                product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                product.Supplier = suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
            }

            return View("Products", products);
        }



        [Route("Show/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var categories = await _categoryRepository.GetAll();
            var suppliers = await _supplierRepository.GetAll();
            var product = await _productRepository.GetById(id);
            product.Category = categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            product.Supplier = suppliers.FirstOrDefault(s => s.SupplierId == product.SupplierId);
            return View(product);
        }

        [Route("Show/About")]
        public IActionResult About()
        {
            return View("About");
        }
        [Route("Show/Contact")]
        public IActionResult Contact()
        {
            return View("Contact"); 
        }
    }
}
