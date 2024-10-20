using CoffeeManagementSystem.Entities.Models;
using CoffeeManagementSystem.Repositories.Context;
using CoffeeManagementSystem.Repositories.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

   


    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newUser = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                EmailConfirmed =true,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)    
            {
                await _userManager.AddToRoleAsync(newUser ,"User");
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }


    public IActionResult Login()
    {
        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Login(LoginViewModel model)
	{
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Rememberme, false);

            if (result.Succeeded )
            { 
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
  

        }
		return View(model);
	}


	[HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult AccessDenied() {
        return View();
    }


}
