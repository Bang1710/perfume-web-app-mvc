using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;

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

            builder.Services.AddDbContext<PerfumeDBContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("PerfumeDBConnect")));

            builder.Services.Configure<RouteOptions>(options => {
                options.AppendTrailingSlash = false;        // Thêm d?u / vào cu?i URL
                options.LowercaseUrls = false;               // url ch? th??ng
                options.LowercaseQueryStrings = false;      // không b?t query trong url ph?i in th??ng
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}