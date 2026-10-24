using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSD_Lab1EB.Data;

namespace SSD_Lab1EB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Configure Identity to use ApplicationUser and support roles.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            // load seeded-user pw from User Secrets            
            var secrets = builder.Configuration.GetSection("Secrets").Get<AppSecrets>();
            if (secrets != null) { DbInitializer.SetAppSecrets(secrets); }


            // seed the DB with initial roles and users
            using (var scope = app.Services.CreateScope())
            {
                DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
