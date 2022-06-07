using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie {
    public class VideoGamesController : Controller {
        private readonly MvcMovieContext _context;

        public VideoGamesController(MvcMovieContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber) {
            if (_context.VideoGame != null) {
                var videoGames = from vg in _context.VideoGame select vg;

                int pageSize = 5;
                return View(await PaginatedList<VideoGame>.CreateAsync(videoGames.AsNoTracking(), pageNumber ?? 1, pageSize));
            } else {
                return Problem("Entity set 'MvcMovieContext.VideoGame' is null.");
            }
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null ||
                _context.VideoGame == null) {
                return NotFound();
            }

            var videoGame = await _context.VideoGame.FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null) {
                return NotFound();
            }

            return View(videoGame);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Rating,ReleaseDate,Genre,Price")] VideoGame videoGame) {
            if (ModelState.IsValid) {
                _context.Add(videoGame);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(videoGame);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.VideoGame == null) {
                return NotFound();
            }

            var videoGame = await _context.VideoGame.FindAsync(id);
            if (videoGame == null) {
                return NotFound();
            }

            return View(videoGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Rating,ReleaseDate,Genre,Price")] VideoGame videoGame) {
            if (id != videoGame.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(videoGame);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!VideoGameExists(videoGame.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(videoGame);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.VideoGame == null) {
                return NotFound();
            }

            var videoGame = await _context.VideoGame.FirstOrDefaultAsync(m => m.Id == id);
            if (videoGame == null) {
                return NotFound();
            }

            return View(videoGame);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.VideoGame == null) {
                return Problem("Entity set 'MvcMovieContext.VideoGame'  is null.");
            }

            var videoGame = await _context.VideoGame.FindAsync(id);
            if (videoGame != null) {
                _context.VideoGame.Remove(videoGame);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoGameExists(int id) {
            return (_context.VideoGame?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
