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
    public class IndexModel : PageModel
    {
        private readonly DAL.Db.AppDbContext _context;
        private readonly IGameRepository _repo;

        public IndexModel(DAL.Db.AppDbContext context, IGameRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        public IList<CheckersGame> CheckersGame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            CheckersGame = _repo.GetAllGames();
            
        }
    }
}
