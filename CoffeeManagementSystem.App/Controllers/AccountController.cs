using CoffeeManagementSystem.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // GET: Login method
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Customer model)
    {
        if (ModelState.IsValid)
        {
            // استرجاع المستخدم من قاعدة البيانات
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                // التحقق من كلمة المرور
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Page404");
                }
            }

            // إضافة خطأ في حالة الفشل
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        // إذا كان هناك خطأ، أعد عرض النموذج مع الأخطاء
        return View(model);
    }

}
