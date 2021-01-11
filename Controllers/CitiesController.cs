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
    public class CitiesController : Controller
    {
        private readonly BdContext _context;

        public CitiesController(BdContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cities == null)
            {
                return NotFound();
            }

            return View(cities);
        }

        // GET: Cities/Create
        public async Task<IActionResult> Create()
        {
/*            ViewBag.Countries = _context.Countries;
*//*            return View(await _context.Countries.ToListAsync());
*/        
        return View();

        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
//        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Area,Student_Count,Info,Id")] Cities cities, int countryId)
        {
            cities.Country = _context.Countries.Find(countryId);
            Console.WriteLine(cities.Country.Name);

            var results = new List<ValidationResult>();
            var context_isValid = new ValidationContext(cities);

            if (!Validator.TryValidateObject(cities, context_isValid, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                _context.Add(cities);
                await _context.SaveChangesAsync();
                Console.WriteLine("new city is valid!");

            }
            /*if (ModelState.IsValid)
            {
                _context.Add(cities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.er
            }*/
            return View(cities);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            return View(cities);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Area,Student_Count,Info,Id")] Cities cities)
        {
            if (id != cities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitiesExists(cities.Id))
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
            return View(cities);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cities == null)
            {
                return NotFound();
            }

            return View(cities);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cities = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(cities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitiesExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
