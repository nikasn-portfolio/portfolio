using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Db;
using Domain;

namespace WebApp.Pages.CheckersOptions
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.Db.AppDbContext _context;
        private readonly IGameOptionsRepository _repoOptions;


        public DeleteModel(DAL.Db.AppDbContext context, IGameOptionsRepository repoOptions)
        {
            _context = context;
            _repoOptions = repoOptions;
        }

        [BindProperty]
      public CheckersOption CheckersOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CheckersOptions == null)
            {
                return NotFound();
            }

            var checkersoption = await _context.CheckersOptions.FirstOrDefaultAsync(m => m.Id == id);

            if (checkersoption == null)
            {
                return NotFound();
            }
            else 
            {
                CheckersOption = checkersoption;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CheckersOptions == null)
            {
                return NotFound();
            }
            var checkersoption = await _context.CheckersOptions.FindAsync(id);

            if (checkersoption != null)
            {
                CheckersOption = checkersoption;
                _repoOptions.DeleteGameOptions(id.ToString()!);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
