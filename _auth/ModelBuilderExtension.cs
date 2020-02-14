using System;
using auth.api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace auth.api
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            string ADMIN_ID = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new[] {
                new IdentityRole { Id = ADMIN_ID, Name = "superadmin", NormalizedName = "SUPERADMIN"},
                 new IdentityRole { Id = "admin", Name = "admin", NormalizedName = "admin"},
                new IdentityRole { Name = "user", NormalizedName = "USER"},
                new IdentityRole { Name = "guest", NormalizedName = "GUEST"},
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = ADMIN_ID,
                    UserName = "superadmin",
                    Email = "erp@kmc.solutions",
                    EmailConfirmed = true,
                    NormalizedEmail = "ERP@KMC.SOLUTIONS",
                    NormalizedUserName = "SUPERADMIN",
                    PasswordHash = hasher.HashPassword(null, "Got2groove!"),
                    AzureId = Guid.Parse("816a1383-0cbc-41d8-ba52-54a49927bd9b"),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ID,
                    UserId = ADMIN_ID
                });



        }

    }
}