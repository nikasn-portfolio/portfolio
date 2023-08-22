using BotBrain;
using DAL;
using DAL.Db;
using Domain;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.CheckersGames;

public class CurrentGame : PageModel
{
    private readonly AppDbContext _context;
    private readonly IGameRepository _repo;
    private readonly IGameStateRepository _gameStateRepository;

    public CurrentGame(AppDbContext context, IGameRepository repo, IGameStateRepository gameStateRepository)
    {
        _context = context;
        _repo = repo;
        _gameStateRepository = gameStateRepository;
    }


    public CheckersBrain Brain { get; set; } = default!;
    public CheckersGame? CheckersGame { get; set; }

    public BrainBot Bot = new BrainBot();
    
    public int? MoveInProcess { get; set; }

    public CheckersGameState? CheckersGameState { get; set; }

    public int? SelectedXHeight { get; set; }

    public int? SelectedYWidth { get; set; }

    public DateTime? GameStateCreatedTime { get; set; }

    public int? DicCount { get; set; }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? DictionaryForDestinationAndForDelete
    {
        get;
        set;
    }


    public async Task<IActionResult> OnGet(int? id, int? x, int? y, int? selectedX, int? selectedY, int? deleteX,
        int? deleteY, string? undo)
    {
        var game = _repo.GetGame(id);

        if (game == null || game.CheckersOption == null)
        {
            return NotFound();
        }

        CheckersGame = game;

        Brain = new CheckersBrain(game.CheckersOption, game.CheckersGameStates!.LastOrDefault());

        if (Brain.ReachedToEndGame())
        {
            game.EndedAt = DateTime.Now;
            if (Brain.NextMoveByBlack())
            {
                game.GameWonPlayer = game.Player2Name;
            }else
            {
                game.GameWonPlayer = game.Player1Name;
            }

            _repo.UpdateOrCreateGame(id!.Value.ToString(), game);
            return Redirect("EndOfGame");
        }

        if (undo == "undo")
        {
            CheckersGameState = _gameStateRepository.UndoState(game);
            if (CheckersGameState == null)
            {
                Brain = new CheckersBrain(game.CheckersOption, game.CheckersGameStates!.LastOrDefault());
                return Page();
            }

            MoveInProcess = CheckersGameState.IsMoveInProcess ? 1 : 0;
            GameStateCreatedTime = game.CheckersGameStates!.LastOrDefault()!.CreatedAt;
            Brain = new CheckersBrain(game.CheckersOption, game.CheckersGameStates!.LastOrDefault());
            return Page();
        }

        if ((Brain.NextMoveByBlack() == false && game.Player2Type == EPlayerType.AI || Brain.NextMoveByBlack() && game.Player1Type == EPlayerType.AI) && selectedX == null &&
            selectedY == null)
        {
            if (DictionaryForDestinationAndForDelete != null && DictionaryForDestinationAndForDelete.Count > 0)
            {
                DictionaryForDestinationAndForDelete =
                    Bot.GetCoordsForMove(Brain, DictionaryForDestinationAndForDelete);
            }
            else
            {
                DictionaryForDestinationAndForDelete = Bot.GetCoordsForMove(Brain, null);
            }

            BoardCoordsForDictionary selectedCoords = Bot.SelectedCheckerCoords!;
            selectedX = selectedCoords.BoardHeightX;
            selectedY = selectedCoords.BoardWidthY;
            Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> randomlyChosenCoords =
                Bot.TakeRandomFromDictionary(DictionaryForDestinationAndForDelete);
            x = randomlyChosenCoords.Keys.First().BoardHeightX;
            y = randomlyChosenCoords.Keys.First().BoardWidthY;
            deleteX = randomlyChosenCoords.Values.First().BoardHeightX;
            deleteY = randomlyChosenCoords.Values.First().BoardWidthY;
        }



        if (selectedX == null && selectedY == null && x != null && y != null && (Brain.NextMoveByBlack()
                && game.Player1Type != EPlayerType.AI ||
                Brain.NextMoveByBlack() == false && game.Player2Type != EPlayerType.AI))
        {
            SelectedXHeight = x;
            SelectedYWidth = y;
            DictionaryForDestinationAndForDelete = Brain.FindPossibleMovesForSelectedChecker(SelectedXHeight.Value, SelectedYWidth.Value);
        }
        else if (x != null && y != null && selectedX != null && selectedY != null && deleteX != null && deleteY != null)
        {
            DictionaryForDestinationAndForDelete = Brain.MakeMove(selectedX.Value, selectedY.Value, x.Value, y.Value,
                deleteX.Value, deleteY.Value);



            if (DictionaryForDestinationAndForDelete!.Count > 0)
            {
                SelectedXHeight = x;
                SelectedYWidth = y;
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> takeRandomCoords =
                    Bot.TakeRandomFromDictionary(DictionaryForDestinationAndForDelete);
                if (CheckersGame.Player2Type == EPlayerType.AI && Brain.NextMoveByBlack() == false || CheckersGame.Player1Type == EPlayerType.AI && Brain.NextMoveByBlack())
                {
                    game.CheckersGameStates!.Add(new CheckersGameState()
                    {
                        // serialize NextMoveByBlack and Board
                        SerializedGameState = Brain.GetSerializedGameState(),
                        IsMoveInProcess = Brain.MoveInProcess
                    });
                    _repo.SaveChanges();
                    return RedirectToPage("./CurrentGame",
                        new
                        {
                            id = CheckersGame.Id, x = takeRandomCoords.Keys.First().BoardHeightX,
                            y = takeRandomCoords.Keys.First().BoardWidthY, selectedX = SelectedXHeight,
                            selectedY = SelectedYWidth, deleteX = takeRandomCoords.Values.First().BoardHeightX,
                            deleteY = takeRandomCoords.Values.First().BoardWidthY
                        });
                }
            }

            if (DictionaryForDestinationAndForDelete.Count == 0 && Brain.LastMoveByKing())
            {
                Brain.SwitchNextMoveByBlack();
                Brain.SwitchLastMoveByKing();
            }

            game.CheckersGameStates!.Add(new CheckersGameState()
            {
                // serialize NextMoveByBlack and Board
                SerializedGameState = Brain.GetSerializedGameState(),
                IsMoveInProcess = Brain.MoveInProcess
            });
            _repo.SaveChanges();
        }

        if (_gameStateRepository.GetStateList(game).LastOrDefault() != null)
        {
            MoveInProcess = _gameStateRepository.GetStateList(game).LastOrDefault()!.IsMoveInProcess ? 1 : 0;
        }
        
        if (Brain.ReachedToEndGame())
        {
            game.EndedAt = DateTime.Now;
            if (Brain.NextMoveByBlack())
            {
                game.GameWonPlayer = game.Player2Name;
            }else
            {
                game.GameWonPlayer = game.Player1Name;
            }

            _repo.UpdateOrCreateGame(id!.Value.ToString(), game);
            return Redirect("EndOfGame");
        }

        if (CheckersGame.Player2Type == EPlayerType.AI && Brain.NextMoveByBlack() == false || CheckersGame.Player1Type == EPlayerType.AI && Brain.NextMoveByBlack())
        {
            return RedirectToPage("./CurrentGame",
                new
                {
                    id = CheckersGame.Id
                });
        }
        return Page();
    }
}