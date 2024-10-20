using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementSystem.App.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
