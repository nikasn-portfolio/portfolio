using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages.CheckersGames;

public class LoadGame : PageModel
{
    private readonly DAL.Db.AppDbContext _context;
    private readonly IGameStateRepository _repo;

    public LoadGame(DAL.Db.AppDbContext context, IGameStateRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        CheckerGame = _context.CheckersGames.Find(id);
        SelectListStates = new SelectList(_repo.GetStateList(CheckerGame!), "Id", "CreatedAt");
        return Page();
    }


    public SelectList SelectListStates { get; set; } = default!;
    [BindProperty] public CheckersGameState CheckersGameState { get; set; } = default!;
    [BindProperty] public CheckersGame? CheckerGame { get; set; }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(CheckersGame? game)
    {
        

        _repo.LoadState(game!, CheckersGameState.Id, null);

        return RedirectToPage("./CurrentGame", new { id = game!.Id });
    }
}