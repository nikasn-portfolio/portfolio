using Domain;

namespace DAL.FileSystemRepository;

public class GameStatesFileSystemRepository : BaseFileSystemRepository,IGameStateRepository
{
    private const string FileExtension = "json";
    private readonly string _statesDirectory = "." + System.IO.Path.DirectorySeparatorChar + "states";
    public string? Name { get; }
    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
    
    private void CheckOrCreateDirectory()
    {
        if (!System.IO.Directory.Exists(_statesDirectory))
        {
            System.IO.Directory.CreateDirectory(_statesDirectory);
        }
    }
    
    public string GetFileName(string id)
    {
        return _statesDirectory +
               System.IO.Path.DirectorySeparatorChar +
               id + "." + FileExtension;
    }

    public void SaveState(CheckersGame game,CheckersGameState? state)
    {
        CheckOrCreateDirectory();
        var loop = true;
        do
        {
            state!.Id = int.Parse(HelperMethods.JsonConverterForBoard.IdGenerator());
            foreach (var fileName in Directory.GetFileSystemEntries(_statesDirectory, "*." + FileExtension))
            {
                if(fileName.Contains(state.Id.ToString()))
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
        
        var fileContent = System.Text.Json.JsonSerializer.Serialize(state);
        System.IO.File.WriteAllText(GetFileName(state.Id.ToString()), fileContent);
    }

    public List<CheckersGameState> GetStateList(CheckersGame game)
    {
        CheckOrCreateDirectory();
        
        
        var res = new List<CheckersGameState>();
        
        foreach (var fileName in Directory.GetFileSystemEntries(_statesDirectory, "*." + FileExtension))
        {
            var fileContent = System.IO.File.ReadAllText(fileName);
            var state = System.Text.Json.JsonSerializer.Deserialize<CheckersGameState>(fileContent);
            if (state!.CheckersGame!.Id.ToString() == game.Id.ToString())
            {
                res.Add(state!);
            }
            
        }
        var sorted = res.OrderBy(x => x.CreatedAt).ToList();
        return sorted;
    }

    public CheckersGameState UndoState(CheckersGame game)
    {
        throw new NotImplementedException();
    }

    public CheckersGameState GetLastState(CheckersGame game)
    {
        return GetStateList(game).LastOrDefault()!;
    }
    
    public CheckersClassStateForSerialization? GetGameState(int id)
    {
        var fileContent = System.IO.File.ReadAllText(GetFileName(id.ToString()));
        var state = System.Text.Json.JsonSerializer.Deserialize<CheckersClassStateForSerialization>(fileContent);
        
        if (state == null)
        {
            throw new NullReferenceException($"Could not deserialize: {fileContent}");
        }

        return state;
    }

    public CheckersGameState LoadState(CheckersGame game, int stateId, DateTime? createdDate)
    {
        var listOfStates = GetStateList(game);
        if(listOfStates.Count == 0)
        {
            return null!;
        }
        foreach (var gameState in listOfStates)
        {
            if(gameState.CreatedAt > createdDate)
                System.IO.File.Delete(GetFileName(gameState.Id.ToString()));
        }

        return GetStateList(game).LastOrDefault()!;
    }

    public void DeleteState(int? id)
    {
        System.IO.File.Delete(GetFileName(id.ToString()!));
    }
}