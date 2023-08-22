using Domain;
using GameBrain;
using Exception = System.Exception;

namespace DAL.FileSystemRepository;

public class GameFileSystemRepository : BaseFileSystemRepository, IGameRepository
{
    private const string FileExtension = "json";
    private readonly string _gamesDirectory = "." + System.IO.Path.DirectorySeparatorChar + "games";

    private string GetFileName(string id)
    {
        return _gamesDirectory +
               System.IO.Path.DirectorySeparatorChar +
               id + "." + FileExtension;
    }

    private void CheckOrCreateDirectory()
    {
        if (!System.IO.Directory.Exists(_gamesDirectory))
        {
            System.IO.Directory.CreateDirectory(_gamesDirectory);
        }
    }

    public string? Name { get; }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public CheckersGame UpdateOrCreateGame(string? id, CheckersGame game)
    {
        CheckOrCreateDirectory();
        var loop = true;
        do
        {
            game.Id = int.Parse(HelperMethods.JsonConverterForBoard.IdGenerator());
            foreach (var fileName in Directory.GetFileSystemEntries(_gamesDirectory, "*." + FileExtension))
            {
                if(fileName.Contains(game.Id.ToString()))
                {
                    loop = false;
                    break;
                }
            }

            if (loop == false)
            {
                loop = true;
            }
            else
            {
                loop = false;
            }
        } while (loop);
        
        
        var fileContent = System.Text.Json.JsonSerializer.Serialize(game);
        System.IO.File.WriteAllText(GetFileName(game.Id.ToString()), fileContent);
        return game;
    }

    public List<CheckersGame> GetAllGames()
    {
        CheckOrCreateDirectory();


        var res = new List<CheckersGame>();

        foreach (var fileName in Directory.GetFileSystemEntries(_gamesDirectory, "*." + FileExtension))
        {
            var fileContent = System.IO.File.ReadAllText(fileName);
            var game = System.Text.Json.JsonSerializer.Deserialize<CheckersGame>(fileContent);
            res.Add(game!);
        }

        return res;
    }

    public CheckersGame? GetGame(int? id)
    {
        var fileContent = System.IO.File.ReadAllText(GetFileName(id.ToString()));
        var game = System.Text.Json.JsonSerializer.Deserialize<CheckersGame>(fileContent);
        if (game == null)
        {
            throw new NullReferenceException($"Could not deserialize: {fileContent}");
        }

        return game;
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
        GameStatesFileSystemRepository gameStatesFileSystemRepository = new GameStatesFileSystemRepository();
        var listOfGames = GetAllGames();
        var game = listOfGames.FirstOrDefault(x => x.Id == id);
        if (game != null)
        {
            var listOfStates = gameStatesFileSystemRepository.GetStateList(game);
            foreach (var state in listOfStates)
            {
                gameStatesFileSystemRepository.DeleteState(state.Id);
            }
        }
        System.IO.File.Delete(GetFileName(id!.Value.ToString()));
    }
}