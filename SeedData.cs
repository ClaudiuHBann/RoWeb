using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;

using MvcMovie.Data;

namespace MvcMovie.Models {
    public static class SeedData {
        private static readonly Random _random = new();
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RandomString(int length) {
            return new string(Enumerable.Repeat(alphabet, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime RandomDateTime() {
            DateTime start = new(1970, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        public static void InitializeMovies(IServiceProvider serviceProvider) {
            using var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            if (context.Movie == null) {
                return;
            }

            /*foreach (var m in context.Movie) {
                context.Remove(m);
            }
            context.SaveChanges();*/

            if (context.Movie.Any()) {
                return;
            }

            for (int i = 0; i < _random.Next(18, 69); i++) {
                context.Add(
                    new Movie {
                        Title = RandomString(18),
                        ReleaseDate = RandomDateTime(),
                        Genre = RandomString(10),
                        Price = (decimal)_random.NextDouble() * 100.0M
                    }
                    );
            }
            context.SaveChanges();
        }

        public static void InitializeVideoGames(IServiceProvider serviceProvider) {
            using var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            if (context.VideoGame == null) {
                return;
            }

            /*foreach (var vg in context.VideoGame) {
                context.Remove(vg);
            }
            context.SaveChanges();*/

            if (context.VideoGame.Any()) {
                return;
            }

            for (int i = 0; i < _random.Next(18, 69); i++) {
                context.Add(
                    new VideoGame {
                        Title = RandomString(18),
                        Description = RandomString(25),
                        Rating = (byte)_random.Next(0, 10),
                        ReleaseDate = RandomDateTime(),
                        Genre = RandomString(10),
                        Price = (decimal)_random.NextDouble() * 100.0M
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}