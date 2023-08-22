using Domain;

namespace DAL;

public interface IGameStateRepository : IBaseRepository
{
    CheckersClassStateForSerialization? GetGameState(int id);
    void SaveState(CheckersGame game, CheckersGameState? state);

    List<CheckersGameState> GetStateList(CheckersGame game);

    CheckersGameState UndoState(CheckersGame game);
    CheckersGameState GetLastState(CheckersGame game);

    CheckersGameState LoadState(CheckersGame game, int stateId, DateTime? createdDate);
    
    void DeleteState(int? id);

}