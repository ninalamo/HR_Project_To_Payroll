using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class IdentityApplicationDbContext : IdentityDbContext
    {
        public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options)
            : base(options)
        {
        }

       
    }

    public static class IdentityInitializer
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("johndoe@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "nin.alamo@outlook.com",
                    EmailConfirmed = true,
                    Id = "developer",
                    LockoutEnabled = false,
                    NormalizedEmail = "NIN.ALAMO@OUTLOOK.COM",
                    NormalizedUserName = "superadmin",
                    PhoneNumber = "09053139149",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    UserName = "superadmin"
                };

                IdentityResult result = userManager.CreateAsync(user, "Got2groove!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "superadmin").Wait();
                }
            }


           
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("superadmin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "superadmin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("hradmin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "hradmin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("approver").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "approver";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("user").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "user";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
