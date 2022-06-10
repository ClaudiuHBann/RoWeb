using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie.Controllers {
    public class MoviesController : Controller {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber) {
            if (_context.Movie != null) {
                var movies = from m in _context.Movie select m;

                int pageSize = 5;
                return View(await PaginatedList<Movie>.CreateAsync(movies.AsNoTracking(), pageNumber ?? 1, pageSize));
            } else {
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }
        }

        // Movie's review's form post method
        [HttpPost]
        public IActionResult PostReview(int? id, string username, string content) {
            if (id == null) {
                return RedirectToAction("Index");
            }

            _context.Add(
                    new Review {
                        MovieId = (int)id,
                        Username = username,
                        PostDate = DateTime.Now,
                        Content = content
                    }
                    );
            _context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null ||
                _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) {
                return NotFound();
            }

            // We get all the reviews of that movie and store them as a list in the view's data
            var reviews =
                from r
                in _context.Review
                where r.MovieId == id
                select r;

            ViewData["reviews"] = reviews.ToList();

            return View(movie);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
            if (ModelState.IsValid) {
                _context.Add(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null ||
                _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null) {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
            if (id != movie.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!MovieExists(movie.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null ||
                _context.Movie == null) {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Movie == null) {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie != null) {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id) {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
