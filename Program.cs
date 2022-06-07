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
                SeedData.InitializeMovies(services);
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

/*
    TO DO:
---------------------------------------------------------------------
        2) De adaugat posibilitatea de a insera recenzii / review-uri.
        
        In view-ul de detalii, imediat sub proprietatile unui movie, se va implementa un tabel cu review-urile utilizatorilor (nume utilizator, data completarii si recenzia        propriu-zisa) iar sub tabel, se vor adauga 3 elemente noi pentru adaugare de review:
            - un input unde se va completa numele persoanei care face review-ul;
            - un input unde se va completa review-ul propriu-zis;
            - un buton pentru inserarea acestuia.
        
        De indata ce se face inserarea, tabelul cu recenzii trebuie sa fie actualizat si sa afiseze si noua inregistrare inserata
---------------------------------------------------------------------

        - generate more realistic data
 */