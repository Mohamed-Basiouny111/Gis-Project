using Gis_Project.Context;
using Gis_Project.Models;
using Gis_Project.Repositories;
using Gis_Project.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gis_Project
{
    public class Program
    {
        public static async  Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add User DI services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            //register identity services 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 4;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<GisContext>();

            //DBContext
            builder.Services.AddDbContext<GisContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
                op.UseLazyLoadingProxies();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using var scope = app.Services.CreateScope();

            //Apply migrations at startup
            var dbContext = scope.ServiceProvider.GetRequiredService<GisContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {

                await dbContext.Database.MigrateAsync();
                await SeedData.SeedData.SeedAppAsync(userManager, userRole, dbContext);

                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogInformation("Migrate and seed data succeeded.");
            }
            catch (Exception ex)
            {
                //Log errors or handle them as needed
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error occurred while migrating the database.");
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

            app.MapControllerRoute(
            name: "ForUserRole",
            pattern: "{controller=Home}/{action=Index}/{userId?}/{roleName?}");


            app.Run();
        }
    }
}
