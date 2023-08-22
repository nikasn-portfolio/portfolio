using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Db;
using Domain;

namespace WebApp.Pages.CheckersOptions
{
    public class CreateModel : PageModel
    {
        private readonly DAL.Db.AppDbContext _context;

        public CreateModel(DAL.Db.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public CheckersOption CheckersOption { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CheckersOptions == null || CheckersOption == null)
            {
                return Page();
            }

            if (CheckersOption.Width < 4 || CheckersOption.Height < 4)
            {
                ModelState.AddModelError("CheckersOption.Width", "Width and Height must be at least 4");
                return Page();
            }

            _context.CheckersOptions.Add(CheckersOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}