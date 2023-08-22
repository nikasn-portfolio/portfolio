using Domain;
using GameBrain;

namespace DAL;

public interface IGameRepository : IBaseRepository
{

    CheckersGame UpdateOrCreateGame(string? id, CheckersGame game);

    List<CheckersGame> GetAllGames();

    CheckersGame? GetGame(int? id);

    string GetGameTurn(CheckersBrain brain, CheckersGame game);
    
    void DeleteGame(int? id);


}