using Microsoft.EntityFrameworkCore;

using MvcMovie.Models;

namespace MvcMovie.Data {
    public class MvcMovieContext : DbContext {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options) { }

        public DbSet<VideoGame>? VideoGame { get; set; }

        public DbSet<Movie>? Movie { get; set; }
        public DbSet<Review>? Review { get; set; }
    }
}
