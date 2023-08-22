using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Db;
using Domain;

namespace WebApp.Pages.CheckersGames
{
    public class CreateModel : PageModel
    {
        private readonly DAL.Db.AppDbContext _context;
        private readonly IGameRepository _repo;

        public CreateModel(DAL.Db.AppDbContext context, IGameRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            SelectListOptions = new SelectList(_context.CheckersOptions, "Id", "Name");
            return Page();
        }

        [BindProperty] public CheckersGame CheckersGame { get; set; } = default!;

        public SelectList SelectListOptions { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CheckersGames == null || CheckersGame == null)
            {
                return Page();
            }

            _repo.UpdateOrCreateGame(null, CheckersGame);
            Console.WriteLine();
            return Redirect($"CurrentGame?id={CheckersGame.Id}");
        }
    }
}