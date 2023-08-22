using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Db;

public class GameOptionsRepositoryDb : BaseRepository,IGameOptionsRepository
{
    public GameOptionsRepositoryDb(AppDbContext dbContext) : base(dbContext)
    {
    }

    public List<CheckersOption> GetGameOptionsList()
    {
        var res    =    Ctx
            .CheckersOptions
            .Include(o => o.CheckersGames)
            .OrderBy(o => o.Name)
            .ToList();

        return res;

    }

    public CheckersOption GetGameOptions(int id)
    {
        var optionsFromDb = Ctx.CheckersOptions.FirstOrDefault(o => o.Id == id);
        if (optionsFromDb == null)
        {
            throw new Exception("No such option");
        }

        return optionsFromDb;
    }

    public void SaveGameOptions(string id, CheckersOption option)
    {
        var optionsFromDb = Ctx.CheckersOptions.FirstOrDefault(o => o.Name == id);
        if (optionsFromDb == null)
        {
            Ctx.CheckersOptions.Add(option);
            Ctx.SaveChanges();
            return;
        }

        optionsFromDb.Name = option.Name;
        optionsFromDb.Width = option.Width;
        optionsFromDb.Height = option.Height;
        optionsFromDb.WhiteStarts = option.WhiteStarts;

        Ctx.SaveChanges();

    }

    public void DeleteGameOptions(string id)
    {
        var optionsFromDb = Ctx.CheckersOptions.Include(o => o.CheckersGames!).ThenInclude(g => g.CheckersGameStates).FirstOrDefault(o => o.Id == int.Parse(id));
        if (optionsFromDb == null)
        {
            throw new Exception("No such option");
        }
        Ctx.CheckersOptions.Remove(optionsFromDb);
        Ctx.SaveChanges();
        var savingsListWhereNull = Ctx.CheckersGameStates.Where(s => s.CheckersGame == null).ToList();
        foreach (var state in savingsListWhereNull)
        {
            Ctx.CheckersGameStates.Remove(state);
        }
        Ctx.SaveChanges();
    }

    public void SaveGame(string id, CheckersGame game)
    {
        var gamesFromDb = Ctx.CheckersGames.FirstOrDefault(o => o.Name == id);
        if (gamesFromDb == null)
        {
            Ctx.CheckersGames.Add(game);
            Ctx.SaveChanges();
            return;
        }

        gamesFromDb.Name = game.Name;
        gamesFromDb.Player1Name = game.Player1Name;
        gamesFromDb.Player2Name = game.Player2Name;
        gamesFromDb.Player1Type = game.Player1Type;
        gamesFromDb.Player2Type = game.Player2Type;
        gamesFromDb.CheckersGameStates = game.CheckersGameStates;
        gamesFromDb.StartedAt = game.StartedAt;
        gamesFromDb.CheckersOptionId = game.CheckersOption!.Id;

        Ctx.SaveChanges();
    }

    public CheckersGame GetGame(string id)
    {
        var gamesFromDb = Ctx.CheckersGames.Include(o => o.CheckersOption).FirstOrDefault(o => o.Name == id);
        Console.WriteLine(gamesFromDb!.CheckersOption);
        if (gamesFromDb == null)
        {
            throw new Exception("No such game");
        }

        return gamesFromDb;
    }

    public void SaveState(CheckersGameState state)
    {
        Ctx.CheckersGameStates.Add(state);
        Ctx.SaveChanges();
    }
    

    public List<CheckersGameState> GetStateList(CheckersGame game)
    {
        var state = Ctx.CheckersGameStates.Include(o => o.CheckersGame)
            .Where(o => o.CheckersGame == game).ToList();


        return state;
    }
}