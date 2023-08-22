using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Db;

public class GameStatesRepositoryDb : BaseRepository, IGameStateRepository
{
    public GameStatesRepositoryDb(AppDbContext dbContext) : base(dbContext)
    {
    }

    public CheckersClassStateForSerialization? GetGameState(int id)
    {
        return Ctx.CheckersGameStates
            .Where(x => x.Id == id)
            .Select(x => new CheckersClassStateForSerialization
            {
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefault();
    }

    public void SaveState(CheckersGame game, CheckersGameState? state)
    {
        Ctx.CheckersGameStates.Add(state!);
        Ctx.SaveChanges();
    }

    public CheckersGameState UndoState(CheckersGame game)
    {
        var stateList = Ctx.CheckersGameStates.Include(o => o.CheckersGame).Where(o => o.CheckersGame == game).ToList();
        if (stateList.Count > 0)
        {
            if (Ctx.CheckersGameStates.Include(o => o.CheckersGame).OrderBy(o => o.CreatedAt)
                    .LastOrDefault(o => o.CheckersGame == game)!.IsMoveInProcess == false)
            {
                Ctx.CheckersGameStates.Remove(Ctx.CheckersGameStates.Include(o => o.CheckersGame)
                    .OrderBy(o => o.CreatedAt)
                    .LastOrDefault(o => o.CheckersGame == game)!);
                Ctx.SaveChanges();
                if (Ctx.CheckersGameStates.Include(o => o.CheckersGame).Where(o => o.CheckersGame == game).ToList()
                        .Count == 0) return null!;
                while (Ctx.CheckersGameStates.Include(o => o.CheckersGame).OrderBy(o => o.CreatedAt)
                       .LastOrDefault(o => o.CheckersGame == game)!.IsMoveInProcess)
                {
                    Ctx.CheckersGameStates.Remove(Ctx.CheckersGameStates.Include(o => o.CheckersGame)
                        .OrderBy(o => o.CreatedAt)
                        .LastOrDefault(o => o.CheckersGame == game)!);
                    Ctx.SaveChanges();
                }
            }
            else
            {
                while (Ctx.CheckersGameStates.Include(o => o.CheckersGame).OrderBy(o => o.CreatedAt)
                       .LastOrDefault(o => o.CheckersGame == game)!.IsMoveInProcess)
                {
                    Ctx.CheckersGameStates.Remove(Ctx.CheckersGameStates.Include(o => o.CheckersGame)
                        .OrderBy(o => o.CreatedAt)
                        .LastOrDefault(o => o.CheckersGame == game)!);
                    Ctx.SaveChanges();
                }
            }

            return Ctx.CheckersGameStates.Include(o => o.CheckersGame).OrderBy(o => o.CreatedAt)
                .LastOrDefault(o => o.CheckersGame == game)!;
        }


        return null!;
    }

    public List<CheckersGameState> GetStateList(CheckersGame game)
    {
        var state = Ctx.CheckersGameStates.Include(o => o.CheckersGame)
            .Where(o => o.CheckersGame == game).ToList();


        return state;
    }

    public CheckersGameState? GetLastState(CheckersGame? game)
    {
        var state = Ctx.CheckersGameStates
            .Include(o => o.CheckersGame).OrderBy(o => o.CreatedAt).LastOrDefault(o => o.CheckersGame == game);


        return state;
    }

    public CheckersGameState LoadState(CheckersGame game, int stateId, DateTime? createdDate)
    {
        var state = Ctx.CheckersGameStates.Include(o => o.CheckersGame).FirstOrDefault(o => o.Id == stateId);
        var listOfStates = Ctx.CheckersGameStates.Include(o => o.CheckersGame).Where(o => o.Id > stateId && o.CheckersGame!.Id == game.Id).ToList();
        foreach (var gameState in listOfStates)
        {
            Ctx.CheckersGameStates.Remove(gameState);
        }
        Ctx.SaveChanges();
        return state!;
    }

    public void DeleteState(int? id)
    {
        throw new NotImplementedException();
    }
}