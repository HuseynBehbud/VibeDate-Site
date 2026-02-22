using diplom.Data;
using Microsoft.EntityFrameworkCore;

namespace diplom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<VibeDateDbContext>(options =>
            {
                options.UseSqlServer("Server = DELL\\SQLEXPRESS; Database = VibeDateDB; Trusted_Connection = True; TrustServerCertificate = True; ");
            });
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
