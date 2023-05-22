using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Controllers;

namespace PerfumeWepAppMVC.NET06
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            //builder.Services.AddScoped<CartController>();


            builder.Services.AddDbContext<PerfumeDBContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("PerfumeDBConnect")));

            builder.Services.Configure<RouteOptions>(options => {
                options.AppendTrailingSlash = false;        // Thêm d?u / vào cu?i URL
                options.LowercaseUrls = false;               // url ch? th??ng
                options.LowercaseQueryStrings = false;      // không b?t query trong url ph?i in th??ng
            });


            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); ;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyCustomRoute",
                    pattern: "chi-tiet-san-pham/{id?}",
                    defaults: new { controller = "Product", action = "Detail" }
                );

                // ...
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}