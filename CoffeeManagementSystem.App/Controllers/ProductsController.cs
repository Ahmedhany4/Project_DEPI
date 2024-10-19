using Microsoft.AspNetCore.Mvc;
using CoffeeManagementSystem.Entities.Models;
using CoffeeManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;



namespace CoffeeManagementSystem.App.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private IBaseRepository<Product> _productRepository;
        private IBaseRepository<Category> _categoryRepository;
        private IBaseRepository<Supplier> _supplierRepository;
        private IUploadFile _uploadFile;
        public ProductsController(IBaseRepository<Product> productRepository, IBaseRepository<Category> categoryRepository, IUploadFile uploadFile, IBaseRepository<Supplier> supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _uploadFile = uploadFile;
            _supplierRepository = supplierRepository;
        }

        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            IEnumerable<Product> products;
            products = await _productRepository.GetAll();
            ViewBag.productscount = products.Count();
            return View(products);
        }

      

        public async Task<ActionResult> Create()
        {
            var categories = await _categoryRepository.GetAll();
            var suppliers = await _supplierRepository.GetAll();
            var product = new Product()
            {
                categoryList = categories.ToList(),
                supplierList = suppliers.ToList()
            };
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product item)

        {
            var categories = await _categoryRepository.GetAll();
            item.categoryList = categories.ToList();

            var suppliers = await _supplierRepository.GetAll();
            item.supplierList = suppliers.ToList();
            string fileName = await _uploadFile.UploadFileAsync("\\Images\\ProductsImages\\", file: item.ImageFile);

            item.ProductImage = fileName;

                await _productRepository.AddItem(item);
               return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Details(int id) 
        {
            var product = await _productRepository.GetById(id);
            return View(product);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetAll();
            var suppliers = await _supplierRepository.GetAll();

            Product product = await _productRepository.GetById(id);
            

            // Check if the product is null (not found)
            if (product == null)      
            {
                Response.StatusCode = 404;
                return View("_Page404"); 
            }

            product.categoryList = category.ToList();
            product.supplierList = suppliers.ToList();
   
            return View("EditProduct", product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Product item)
        {

            try
            { 
                if (item.ImageFile != null)
                {
                    string fileName = await _uploadFile.UploadFileAsync("\\Images\\ProductsImages\\", file: item.ImageFile);
                    item.ProductImage = fileName; 
                }
                await _productRepository.UpdateItem(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("EditProduct"); // Return the model to the view
            }
        }



        // GET: ProductsController/Delete/5

        public async Task <ActionResult> Delete(int id)
        {
            // Assuming you have a method to get the product by ID
            var product = await _productRepository.GetById(id); // Replace with your logic to get the product
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Assuming you have a method to delete the product by ID
               await _productRepository.DeleteItem(id); // Replace with your deletion logic

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Handle errors, e.g., log the error or show a user-friendly message
                return View();
            }
        }

    }
}
