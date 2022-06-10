using Microsoft.EntityFrameworkCore;

using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MvcMovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                // We generate random data with Mockaroo(https://www.mockaroo.com)
                SeedData.InitializeMovies(services);
                SeedData.InitializeMoviesReviews(services);

                SeedData.InitializeVideoGames(services);
            }

            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}