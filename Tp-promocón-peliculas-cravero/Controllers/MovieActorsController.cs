using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp_promoción_peliculas_cravero.Models;
using Tp_promocón_peliculas_cravero;

namespace Tp_promocón_peliculas_cravero.Controllers
{
    public class MovieActorsController : Controller
    {
        private readonly DbConection _context;

        public MovieActorsController(DbConection context)
        {
            _context = context;
        }

        // GET: MovieActors
        public async Task<IActionResult> Index()
        {
            var dbConection = _context.MoviesActor.Include(m => m.Film).Include(m => m.Person);
            return View(await dbConection.ToListAsync());
        }

        // GET: MovieActors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MoviesActor
                .Include(m => m.Film)
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (movieActor == null)
            {
                return NotFound();
            }

            return View(movieActor);
        }

        // GET: MovieActors/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "Title");
            ViewData["PersonId"] = new SelectList(_context.Actor, "Id", "Name");
            return View();
        }

        // POST: MovieActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmId,PersonId")] MovieActor movieActor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieActor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "ReleaseDate", movieActor.FilmId);
            ViewData["PersonId"] = new SelectList(_context.Actor, "Id", "Biography", movieActor.PersonId);
            return View(movieActor);
        }

        // GET: MovieActors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MoviesActor.FindAsync(id);
            if (movieActor == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "ReleaseDate", movieActor.FilmId);
            ViewData["PersonId"] = new SelectList(_context.Actor, "Id", "Biography", movieActor.PersonId);
            return View(movieActor);
        }

        // POST: MovieActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,PersonId")] MovieActor movieActor)
        {
            if (id != movieActor.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieActor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieActorExists(movieActor.FilmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "ReleaseDate", movieActor.FilmId);
            ViewData["PersonId"] = new SelectList(_context.Actor, "Id", "Biography", movieActor.PersonId);
            return View(movieActor);
        }

        // GET: MovieActors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MoviesActor
                .Include(m => m.Film)
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (movieActor == null)
            {
                return NotFound();
            }

            return View(movieActor);
        }

        // POST: MovieActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieActor = await _context.MoviesActor.FindAsync(id);
            _context.MoviesActor.Remove(movieActor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieActorExists(int id)
        {
            return _context.MoviesActor.Any(e => e.FilmId == id);
        }
    }
}
