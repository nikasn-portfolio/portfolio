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

namespace WebApp.Pages.CheckersGames
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.Db.AppDbContext _context;
        private readonly IGameOptionsRepository _repoOptions;

        public DetailsModel(DAL.Db.AppDbContext context, IGameOptionsRepository repoOptions)
        {
            _context = context;
            _repoOptions = repoOptions;
        }

      public CheckersGame CheckersGame { get; set; } = default!; 

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null || _context.CheckersGames == null)
            {
                return NotFound();
            }

            var checkersGame = await _context.CheckersGames.FindAsync(id);
            if (checkersGame == null)
            {
                return NotFound();
            }
            _repoOptions.DeleteGameOptions(id.ToString()!);
            
            return Page();
        }
    }
}
