using Microsoft.EntityFrameworkCore;
using ContextFile;
using CoffeeManagementSystem.Repositories.Interfaces;
using CoffeeManagementSystem.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;

using CoffeeManagementSystem.Repositories.Emplimintations;
using CoffeeManagementSystem.Repositories.Context;
//using CoffeeManagementSystem.Repositories.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework to use SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedEmail = false;

    }

).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();


// Configure cookie authentication

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to this path if access is denied
});

// Register your repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUploadFile, UploadFile>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
