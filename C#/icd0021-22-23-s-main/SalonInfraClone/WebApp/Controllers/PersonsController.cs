using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.App;

namespace WebApp.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Persons.Include(p => p.AppUser).Include(p => p.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName");
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,AppUserId,PersonName,PersonSurname,PersonPhoneNumber,PersonBirthDate,Id")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", person.AppUserId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", person.LanguageId);
            return View(person);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", person.AppUserId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", person.LanguageId);
            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LanguageId,AppUserId,PersonName,PersonSurname,PersonPhoneNumber,PersonBirthDate,Id")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", person.AppUserId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", person.LanguageId);
            return View(person);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(p => p.AppUser)
                .Include(p => p.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Persons'  is null.");
            }
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(Guid id)
        {
          return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
