using GameStore.Data;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlite(
                    builder.Configuration.GetConnectionString("StoreConnection"));
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreDbContext>();

            var app = builder.Build();

            if (app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);

            app.Run();
        }
    }
}