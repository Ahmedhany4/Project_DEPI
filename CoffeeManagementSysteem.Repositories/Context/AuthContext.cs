using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CoffeeManagementSystem.Repositories.Context
{
    public class AuthContext :IdentityDbContext<MyIdentityUser>
    {
        public AuthContext(DbContextOptions<AuthContext> options):base(options)
        {

            
        }
    }
    public class MyIdentityUser :IdentityUser
    {
        
    }
}
