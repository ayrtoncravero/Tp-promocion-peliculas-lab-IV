using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp_promoción_peliculas_cravero.Models;
using Tp_promocón_peliculas_cravero;

namespace Tp_promocón_peliculas_cravero.Controllers
{
    public class FilmsController : Controller
    {
        private readonly DbConection _context;
        private readonly IWebHostEnvironment env;

        public FilmsController(DbConection context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var dbConection = _context.Film.Include(f => f.Genders);
            return View(await dbConection.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Genders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Description");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Photo,Trailer,Summary,GenderId,billboard")] Film film)
        {
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files;
                if(file != null && file.Count > 0)
                {
                    var filePhoto = file[0];
                    var pathDestine = Path.Combine(env.WebRootPath, "Image\\Movies");

                    if(filePhoto.Length > 0)
                    {
                        var fileDestine = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(filePhoto.FileName);

                        using (var filestrem = new FileStream(Path.Combine(pathDestine, fileDestine), FileMode.Create))
                        {
                            filePhoto.CopyTo(filestrem);
                            film.Photo = fileDestine;
                        };
                    }
                }

                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Description", film.GenderId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Description", film.GenderId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Photo,Trailer,Summary,GenderId,billboard,Photo")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Para que guarde la foto al editar
                var file = HttpContext.Request.Form.Files;
                if (file != null && file.Count > 0)
                {
                    var filePhoto = file[0];
                    var pathDestine = Path.Combine(env.WebRootPath, "Image\\Movies");

                    if (filePhoto.Length > 0)
                    {
                        var fileDestine = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(filePhoto.FileName);

                        using (var filestrem = new FileStream(Path.Combine(pathDestine, fileDestine), FileMode.Create))
                        {
                            filePhoto.CopyTo(filestrem);

                            //Para eliminar la foto anterior por la nueva
                            string oldFile = Path.Combine(pathDestine, film.Photo ?? null);
                            if (System.IO.File.Exists(oldFile))
                                System.IO.File.Delete(oldFile);
                            film.Photo = fileDestine;
                            
                        };
                    }
                }

                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Description", film.GenderId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Genders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
