﻿using Microsoft.AspNetCore.Identity;


namespace CoffeeManagementSystem.Repositories.Context
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
