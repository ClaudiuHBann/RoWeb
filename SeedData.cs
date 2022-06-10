using System.Runtime.CompilerServices;
using System.Net;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using MvcMovie.Data;

namespace MvcMovie.Models {
    public static class SeedData {
        private static readonly Random _random = new();

        // Generates random realistic movies
        public static void InitializeMovies(IServiceProvider serviceProvider) {
            using var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            if (context.Movie == null) {
                return;
            }

            // We delete the current movies
            foreach (var m in context.Movie) {
                context.Remove(m);
            }
            context.SaveChanges();

            // We generate a random number of movies and get the json with the movies data
            int rows = _random.Next(18, 69);
            var json = GetRandomMockaroo("59aa1ac0", rows);
            if (json == null) {
                return;
            }

            // We create the movies and update the database
            for (int i = 0; i < rows; i++) {
                context.Add(
                    new Movie {
                        Title = json[i].Title,
                        ReleaseDate = DateTime.Parse($"{json[i].ReleaseDate} {json[i].ReleaseTime}"),
                        Genre = json[i].Genre,
                        Price = json[i].Price
                    }
                    );
            }
            context.SaveChanges();
        }

        // Generates random realistic movies reviews
        public static void InitializeMoviesReviews(IServiceProvider serviceProvider) {
            using var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            if (context.Review == null) {
                return;
            }

            // We delete the current movies reviews
            foreach (var r in context.Review) {
                context.Remove(r);
            }
            context.SaveChanges();

            // We get the movies ids
            var moviesIds = from m in context.Movie select m.Id;
            var moviesIdsArray = moviesIds.ToArray();

            // We generate a random number of movies reviews and get the json with the movies reviews data
            int rows = _random.Next(200, 500);
            var json = GetRandomMockaroo("6d3f10a0", rows);
            if (json == null) {
                return;
            }

            // We create the movies reviews and update the database
            for (int i = 0; i < rows; i++) {
                context.Add(
                    new Review {
                        MovieId = _random.Next(moviesIdsArray[0], moviesIdsArray[^1] + 1),
                        Username = json[i].Username,
                        PostDate = DateTime.Parse($"{json[i].PostDate} {json[i].PostTime}"),
                        Content = json[i].Content
                    }
                    );
            }
            context.SaveChanges();
        }

        // Generates random video games data
        public static void InitializeVideoGames(IServiceProvider serviceProvider) {
            using var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            if (context.VideoGame == null) {
                return;
            }

            // We delete the current video games
            foreach (var vg in context.VideoGame) {
                context.Remove(vg);
            }
            context.SaveChanges();

            // We generate a random number of video games and get the json with the video games data
            int rows = _random.Next(18, 69);
            var json = GetRandomMockaroo("76caf240", rows);
            if (json == null) {
                return;
            }

            // We create the video games and update the database
            for (int i = 0; i < rows; i++) {
                context.Add(
                    new VideoGame {
                        Title = json[i].Title,
                        Description = json[i].Description,
                        Rating = json[i].Rating,
                        ReleaseDate = DateTime.Parse($"{json[i].ReleaseDate} {json[i].ReleaseTime}"),
                        Genre = json[i].Genre,
                        Price = json[i].Price
                    }
                    );
            }
            context.SaveChanges();
        }

        // Returns the json from the Mockaroo url
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static dynamic? GetRandomMockaroo(string schema, int rows, string key = "d7ccea10") {
            var response = new WebClient().DownloadString($"https://api.mockaroo.com/api/{schema}?count={rows}&key={key}");
            return JsonConvert.DeserializeObject(response);
        }
    }
}