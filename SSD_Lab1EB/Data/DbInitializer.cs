using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SSD_Lab1EB.Data
{
    /// <summary>
    /// Handles initial DB setup for roles and seeded users.
    /// </summary>
    public static class DbInitializer
    {
        // Stores the sensitive seeded-user passwords loaded from User Secrets.
        private static AppSecrets appSecrets = new AppSecrets();

        /// <summary>
        /// Receives the secrets from Program.cs.
        /// </summary>
        public static void SetAppSecrets(AppSecrets secrets)
        {
            appSecrets = secrets;
        }

        /// <summary>
        /// Applies migrations and creates required roles and users if needed.
        /// </summary>
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Apply pending migrations before seeding.
            context.Database.Migrate();

            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create Supervisor role if it does not already exist.
            if (!await roleManager.RoleExistsAsync("Supervisor"))
            {
                var result =
                    await roleManager.CreateAsync(new IdentityRole("Supervisor"));

                if (!result.Succeeded)
                    return 1;
            }

            // Create Employee role if it does not already exist.
            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                var result =
                    await roleManager.CreateAsync(new IdentityRole("Employee"));

                if (!result.Succeeded)
                    return 2;
            }

            // Create Supervisor user if it does not already exist.
            if (await userManager.FindByEmailAsync("supervisor@lab1.com") == null)
            {
                var supervisorUser = new ApplicationUser
                {
                    UserName = "supervisor@lab1.com",
                    Email = "supervisor@lab1.com",
                    FirstName = "Lab",
                    LastName = "Supervisor",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    supervisorUser,
                    appSecrets.SupervisorPassword);

                if (!result.Succeeded)
                    return 3;

                result = await userManager.AddToRoleAsync(
                    supervisorUser,
                    "Supervisor");

                if (!result.Succeeded)
                    return 4;
            }

            // Create Employee user if it does not already exist.
            if (await userManager.FindByEmailAsync("employee@lab1.com") == null)
            {
                var employeeUser = new ApplicationUser
                {
                    UserName = "employee@lab1.com",
                    Email = "employee@lab1.com",
                    FirstName = "Lab",
                    LastName = "Employee",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    employeeUser,
                    appSecrets.EmployeePassword);

                if (!result.Succeeded)
                    return 5;

                result = await userManager.AddToRoleAsync(
                    employeeUser,
                    "Employee");

                if (!result.Succeeded)
                    return 6;
            }

            return 0;
        }
    }
}