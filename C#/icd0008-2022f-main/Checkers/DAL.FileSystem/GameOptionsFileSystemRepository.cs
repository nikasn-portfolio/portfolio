using Domain;

namespace DAL.FileSystemRepository;

public class GameOptionsFileSystemRepository : BaseFileSystemRepository,IGameOptionsRepository
{
    public string? Name { get; }
    
    private const string FileExtension = "json";
    private readonly string _optionsDirectory = "." + System.IO.Path.DirectorySeparatorChar + "options";
    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public List<CheckersOption> GetGameOptionsList()
    {
        CheckOrCreateDirectory();
        
        
        var res = new List<CheckersOption>();
        
        foreach (var fileName in Directory.GetFileSystemEntries(_optionsDirectory, "*." + FileExtension))
        {
            var fileContent = System.IO.File.ReadAllText(fileName);
            var options = System.Text.Json.JsonSerializer.Deserialize<CheckersOption>(fileContent);
            res.Add(options!);
        }

        return res;
    }

    public void SaveGameOptions(string id, CheckersOption option)
    {
        CheckOrCreateDirectory();
        var loop = true;
        
        do
        {
            option.Id = int.Parse(HelperMethods.JsonConverterForBoard.IdGenerator());
            foreach (var fileName in Directory.GetFileSystemEntries(_optionsDirectory, "*." + FileExtension))
            {
                if(fileName.Contains(option.Id.ToString()))
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
        }while (loop);
        var fileContent = System.Text.Json.JsonSerializer.Serialize(option);
        System.IO.File.WriteAllText(GetFileName(option.Id.ToString()), fileContent);
    }

    public void DeleteGameOptions(string id)
    {
        GameFileSystemRepository gameFileSystemRepository = new GameFileSystemRepository();
        GameStatesFileSystemRepository gameStatesFileSystemRepository = new GameStatesFileSystemRepository();
        System.IO.File.Delete(GetFileName(id));
        var listOfGames = gameFileSystemRepository.GetAllGames();
        CheckersGame? gamePointer = null;
        foreach (var game in listOfGames)
        {
            if (game.CheckersOption!.Id == int.Parse(id))
            {
                gameFileSystemRepository.DeleteGame(game.Id);
            }
        }
    }

    public void SaveState(CheckersGameState state)
    {
        throw new NotImplementedException();
    }
    
    private void CheckOrCreateDirectory()
    {
        if (!System.IO.Directory.Exists(_optionsDirectory))
        {
            System.IO.Directory.CreateDirectory(_optionsDirectory);
        }
    }
    
    public string GetFileName(string id)
    {
        return _optionsDirectory +
               System.IO.Path.DirectorySeparatorChar +
               id + "." + FileExtension;
    }
    
    public CheckersOption GetGameOptions(int id)
    {
        var fileContent = System.IO.File.ReadAllText(GetFileName(id.ToString()));
        var options = System.Text.Json.JsonSerializer.Deserialize<CheckersOption>(fileContent);
        if (options == null)
        {
            throw new NullReferenceException($"Could not deserialize: {fileContent}");
        }

        return options;
    }
}