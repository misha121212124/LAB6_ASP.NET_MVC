using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab6;
using lab6.Models;
using System.ComponentModel.DataAnnotations;

namespace lab6.Controllers
{
    public class CinemasController : Controller
    {
        private readonly BdContext _context;

        public CinemasController(BdContext context)
        {
            _context = context;
        }

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cinemas.ToListAsync());
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemas = await _context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemas == null)
            {
                return NotFound();
            }

            return View(cinemas);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Q_Income,Id")] Cinemas cinemas, int cityId)
        {
            cinemas.City = _context.Cities.Find(cityId);
            Console.WriteLine(cinemas.City.Name);

            var results = new List<ValidationResult>();
            var context_isValid = new ValidationContext(cinemas);

            if (!Validator.TryValidateObject(cinemas, context_isValid, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                _context.Add(cinemas);
                await _context.SaveChangesAsync();
                Console.WriteLine("new cinema is valid!");

            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(cinemas);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View(cinemas);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemas = await _context.Cinemas.FindAsync(id);
            if (cinemas == null)
            {
                return NotFound();
            }
            return View(cinemas);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,Q_Income,Id")] Cinemas cinemas)
        {
            if (id != cinemas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemasExists(cinemas.Id))
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
            return View(cinemas);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemas = await _context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemas == null)
            {
                return NotFound();
            }

            return View(cinemas);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemas = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinemas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemasExists(int id)
        {
            return _context.Cinemas.Any(e => e.Id == id);
        }
    }
}
