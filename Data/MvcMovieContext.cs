using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data {
    public class MvcMovieContext : DbContext {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options) { }

        public DbSet<Models.Movie>? Movie { get; set; }

        public DbSet<MvcMovie.Models.VideoGame>? VideoGame { get; set; }

        public DbSet<MvcMovie.Models.Review>? Review { get; set; }
    }
}
