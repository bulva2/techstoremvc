using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Database;

namespace TechStoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string conString = builder.Configuration.GetConnectionString("MySQL") ?? throw new Exception("Unable to obtain MySQL connection string!");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySQL(conString));

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
