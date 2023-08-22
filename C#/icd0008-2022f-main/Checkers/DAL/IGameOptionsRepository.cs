using Domain;

namespace DAL;

public interface IGameOptionsRepository : IBaseRepository
{
    // crud methods

    // read
    List<CheckersOption> GetGameOptionsList();
    CheckersOption GetGameOptions(int id);
    void SaveGameOptions(string id, CheckersOption option);
    void DeleteGameOptions(string id);
}