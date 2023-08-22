using Domain;
using GameBrain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Db;

public class GamesRepositoryDb : BaseRepository,IGameRepository
{
    public GamesRepositoryDb(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public List<CheckersGame> GetAllGames()
    {
        return Ctx.CheckersGames
            .Include(o => o.CheckersOption)
            .OrderByDescending(o => o.StartedAt)
            .ToList();
    }

    public CheckersGame UpdateOrCreateGame(string? id, CheckersGame game)
    {
        CheckersGame? gameFromDb = null;
        if (id != null)
        {
            gameFromDb = Ctx.CheckersGames.FirstOrDefault(o => o.Id == int.Parse(id!));  
        }
        if (gameFromDb == null)
        {
            Ctx.CheckersGames.Add(game);
            Ctx.SaveChanges();

            return game;
        }
        

        gameFromDb.Name = game.Name;
        gameFromDb.Player1Name = game.Player1Name;
        gameFromDb.Player2Name = game.Player2Name;
        gameFromDb.Player1Type = game.Player1Type;
        gameFromDb.Player2Type = game.Player2Type;
        gameFromDb.CheckersGameStates = game.CheckersGameStates;
        gameFromDb.StartedAt = game.StartedAt;
        gameFromDb.CheckersOptionId = game.CheckersOption!.Id;

        Ctx.SaveChanges();
        return gameFromDb;
    }

    public CheckersGame? GetGame(int? id)
    {
        
        return Ctx.CheckersGames.
            Include(g => g.CheckersOption).
            Include(g => g.CheckersGameStates).
            FirstOrDefault(g => g.Id == id);
            
    }

    public string GetGameTurn(CheckersBrain brain, CheckersGame game)
    {
        if (brain.NextMoveByBlack())
        {
            return $"Black => {game.Player1Name}";
        }

        return $"White => {game.Player2Name}";
    }

    public void DeleteGame(int? id)
    {
        var game = Ctx.CheckersGames.Include(o => o.CheckersGameStates).FirstOrDefault(o => o.Id == id);
        if (game != null)
        {
            Ctx.CheckersGames.Remove(game);
            Ctx.SaveChanges();
        }
        var savings = Ctx.CheckersGameStates.Where(o => o.CheckersGame == null);
        foreach (var state in savings)
        {
            Ctx.CheckersGameStates.Remove(state);
        }
        Ctx.SaveChanges();

    }
}