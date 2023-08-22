using System.Text.Json;
using BotBrain;
using ConsoleUI;
using DAL;
using DAL.Db;
using DAL.FileSystemRepository;
using Domain;
using GameBrain;
using MenuSystem;
using Microsoft.EntityFrameworkCore;


var gameOptions = new CheckersOption();


CheckersGame? gameInfo = new CheckersGame();

CheckersState stateOfBoard = new CheckersState(); // should contain board that goes to the checkersBrain

BoardCoordsForDictionary selectedCheckerCoords = null!;

Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
    new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();

var gameBrain = new CheckersBrain(gameOptions, null);

BrainBot bot = new BrainBot();


var dbOptions =
    new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite("Data Source=/Users/nikitakasnikov/Dev/temp db/checkers.db")
        .Options;

var ctx = new AppDbContext(dbOptions);

IGameRepository gameRepository;

IGameStateRepository gameStateRepository;

IGameOptionsRepository gameOptionsRepository;

var repo = "FS";

if (repo.Equals("DB"))
{
    gameRepository = new GamesRepositoryDb(ctx);

    gameStateRepository = new GameStatesRepositoryDb(ctx);

    gameOptionsRepository = new GameOptionsRepositoryDb(ctx);
}
else
{
    gameRepository = new GameFileSystemRepository();

    gameStateRepository = new GameStatesFileSystemRepository();

    gameOptionsRepository = new GameOptionsFileSystemRepository();
}


var optionsMenu = new Menu(
    EMenuLevel.Other,
    ">  Checkers Game  <",
    null,
    new List<MenuItem>()
    {
        new MenuItem("C", "Delete game", DeleteGame),
        new MenuItem("D", "Delete option", DeleteGameOptions),
    },
    null,
    null
);

Menu mainMenu = null!;
Menu gameMenu = null!;
gameMenu = new Menu(
    EMenuLevel.Second,
    ">  Checkers Game  <",
    null,
    new List<MenuItem>()
    {
        new MenuItem("P", "Make a move", MakeAMove)
    },
    null,
    null
);



mainMenu = new Menu(
    EMenuLevel.Main,
    ">  Checkers Game  <",
    null,
    new List<MenuItem>()
    {
        new MenuItem("N", "New Game", DoNewGame),
        new MenuItem("L", "Load Game", LoadGame),
        new MenuItem("O", "Option", optionsMenu.RunMethod),
    },
    null,
    null
);
mainMenu.RunMethod();

string DeleteGame()
{
    var loop = true;
    List<CheckersGame> gamesList = gameRepository.GetAllGames();
    foreach (var checkersGame in gamesList)
    {
        Console.WriteLine(checkersGame.Id + " - " + checkersGame.Name);
    }
    int id;
    do
    {
        Console.WriteLine("Enter game id to delete");
        while(!int.TryParse(Console.ReadLine(), out id) || id == 0)
        {
            Console.Clear();
            Console.WriteLine("You entered an invalid number");
            Console.Write("Enter Height of the board again: ");
        }

        foreach (var game in gamesList)
        {
            if (game.Id == id)
            {
                loop = false;
            }
        }

        if (loop)
        {
            Console.WriteLine("There is no game with such id" + id);
        }
    } while (loop);
    
    gameRepository.DeleteGame(id);
    return "-";
}

string DeleteGameOptions()
{
    var loop = true;
    List<CheckersOption> optionsList = gameOptionsRepository.GetGameOptionsList();
    foreach (var checkersOption in optionsList)
    {
        Console.WriteLine(checkersOption.Id + " - " + checkersOption.Name);
    }

    int id;
    do
    {
        Console.WriteLine("Enter option id to delete");
        while(!int.TryParse(Console.ReadLine(), out id) || id == 0)
        {
            Console.Clear();
            Console.WriteLine("You entered an invalid number");
            Console.Write("Enter Height of the board again: ");
        }

        foreach (var option in optionsList)
        {
            if(option.Id == id)
            {
                loop = false;
                break;
            }
        }

        if (loop)
        {
            Console.WriteLine("No such option with id " + id);
            Console.WriteLine("Try again");
        }
    } while (loop);
    
    gameOptionsRepository.DeleteGameOptions(id.ToString());
    return "-";
}




string ListGameOptions()
{
    if (gameOptionsRepository.GetGameOptionsList().Count == 0)
    {
        Console.WriteLine("List is empty");
        return "S";
    }

    foreach (var option in gameOptionsRepository.GetGameOptionsList())
    {
        Console.WriteLine(option.Id + "-" + option);
    }

    return "-";
}




string CreateGameOptions()
{
    
    Console.WriteLine("Enter name of the options you want to create: ");

    string fileName = Console.ReadLine()!;
    gameOptions.Name = fileName;

    

    Console.WriteLine("Enter Width of the board");
    int n;

    while(!int.TryParse(Console.ReadLine(), out n) || n < 4)
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid number, at least 4");
        Console.Write("Enter Width of the board again: ");
    }

    gameOptions.Width = n;
    
    Console.WriteLine("Enter Height of the board");

    while(!int.TryParse(Console.ReadLine(), out n) || n < 4)
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid number, at least 4");
        Console.Write("Enter Height of the board again: ");
    }

    gameOptions.Height = n;

    Console.WriteLine("Enter who starts black or white, enter [W] for white and [B] for black");
    var starts = Console.ReadLine()!.ToUpper().Trim();
    while(!starts.Equals("W") && !starts.Equals("B"))
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid letter, enter [W] for white and [B] for black again: ");
        starts = Console.ReadLine()!.ToUpper().Trim();
    }

    if (starts.Equals("W"))
    {
        gameOptions.WhiteStarts = true;
    }
    if (starts.Equals("B"))
    {
        gameOptions.WhiteStarts = false;
    }
    gameOptionsRepository.SaveGameOptions(fileName, gameOptions);
    return gameOptions.Id.ToString();
}

string LoadGame()
{
    List<CheckersGame> gamesList = gameRepository.GetAllGames();
    foreach (var game in gamesList)
    {
        Console.WriteLine(game.Id + "-" + game.Name);
    }

    var loop = true;
    int id;
    do
    {
        Console.WriteLine("Enter game number: ");
        
        while(!int.TryParse(Console.ReadLine(), out id) || id == 0)
        {
            Console.WriteLine("You entered an invalid number");
            Console.Write("Enter game number again: ");
        }

        foreach (var game in gamesList)
        {
            if (game.Id == id)
            {
                loop = false;
            }
        }

        if (loop)
        {
            Console.WriteLine("No such game with id " + id);
            Console.WriteLine("Try again");        }
    }while (loop);
    
    gameInfo = gameRepository.GetGame(id);
    foreach (var state in gameStateRepository.GetStateList(gameInfo!))
    {
        Console.WriteLine(state.Id + " --> " + state.CreatedAt);
    }

    Console.WriteLine("Choose game state to load, insert ID or press [Enter] to skip, last state will be loaded: ");
    string? stateId = Console.ReadLine()?.Trim();
    if (!string.IsNullOrEmpty(stateId))
    {
        gameStateRepository.LoadState(gameInfo!, int.Parse(stateId),
            gameStateRepository.GetGameState(int.Parse(stateId))!.CreatedAt);
    }

    gameOptions = gameInfo!.CheckersOption;
    Console.WriteLine(gameOptions);
    gameBrain = new CheckersBrain(gameOptions!, gameStateRepository.GetLastState(gameInfo));
    if (gameBrain.ReachedToEndGame())
    {
        mainMenu.Title = "> Game is over <";
        mainMenu.RunMethod();
    }
    else
    {
        UI.DrawGameBoard(gameBrain.GetBoard(), null);
        Console.WriteLine(gameRepository.GetGameTurn(gameBrain, gameInfo));
        gameMenu._brain = gameBrain;
        gameMenu._game = gameInfo;
        gameMenu.RunMethod(); 
    }

    return "-";
}

string MakeAMove()
{
    if (dictionaryForDestinationAndForDelete.Count == 0 &&
        (gameInfo.Player2Type != EPlayerType.AI && gameBrain.NextMoveByBlack() == false ||
         gameInfo.Player1Type != EPlayerType.AI && gameBrain.NextMoveByBlack()))
    {
        int y1;
        int x1;
        while (dictionaryForDestinationAndForDelete!.Count == 0)
        {
            Console.WriteLine("Select checker to move");
            Console.WriteLine();
            Console.WriteLine("Enter board width:");
            while(!int.TryParse(Console.ReadLine(), out y1))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write("Enter Width of the board again: ");
            }
            Console.WriteLine("Enter board height:");
            while(!int.TryParse(Console.ReadLine(), out x1))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write("Enter Height of the board again: ");
            }

            if (gameBrain.ReturnsPiece(x1, y1) != null)
            {
                selectedCheckerCoords = new BoardCoordsForDictionary(x1, y1);
                dictionaryForDestinationAndForDelete = gameBrain.FindPossibleMovesForSelectedChecker(x1, y1);
            }else{
                Console.WriteLine("No checker in this position");
            }
            
        }
        
        UI.DrawGameBoard(gameBrain.GetBoard(), dictionaryForDestinationAndForDelete);
    }
    
    
    var x = 0;
    var y = 0;
    var checkForOut = true;
    foreach (var dictionary in dictionaryForDestinationAndForDelete)
    {
        Console.WriteLine($"Coords for Width => {dictionary.Key.BoardWidthY} , Coords for Height => {dictionary.Key.BoardHeightX}");
    }

    while (checkForOut)
    {
        if (gameInfo.Player2Type == EPlayerType.AI && gameBrain.NextMoveByBlack() == false ||
            gameInfo.Player1Type == EPlayerType.AI && gameBrain.NextMoveByBlack())
        {
            dictionaryForDestinationAndForDelete = bot.GetCoordsForMove(gameBrain, dictionaryForDestinationAndForDelete);
            if (dictionaryForDestinationAndForDelete!.Count > 0)
            {
                selectedCheckerCoords = bot.ReturnSelectedCoords(selectedCheckerCoords);
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> randDestination =
                    bot.TakeRandomFromDictionary(dictionaryForDestinationAndForDelete);
                x = randDestination.Keys.First().BoardHeightX;
                y = randDestination.Keys.First().BoardWidthY;
            }
        }
        else
        {
            Console.WriteLine("Enter board width:");
            while(!int.TryParse(Console.ReadLine(), out y))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write("Enter Width of the board again: ");
            }
            Console.WriteLine("Enter board height:");
            while(!int.TryParse(Console.ReadLine(), out x))
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid number");
                Console.Write("Enter Height of the board again: ");
            }
        }

        foreach (var coords in dictionaryForDestinationAndForDelete)
        {
            if (y == coords.Key.BoardWidthY && x == coords.Key.BoardHeightX)
            {
                checkForOut = false;
                dictionaryForDestinationAndForDelete = gameBrain.MakeMove(selectedCheckerCoords.BoardHeightX,
                    selectedCheckerCoords.BoardWidthY, x, y,
                    coords.Value.BoardHeightX, coords.Value.BoardWidthY);
                if (dictionaryForDestinationAndForDelete!.Count > 0)
                {
                    selectedCheckerCoords = new BoardCoordsForDictionary(x, y);
                }
            }
        }

        if (checkForOut)
        {
            Console.WriteLine("Enter Width and Height again, can't make move");
        }
    }
    

    gameStateRepository.SaveState(gameInfo, new CheckersGameState()
    {
        // serialize NextMoveByBlack and Board,
        CheckersGame = gameInfo,
        SerializedGameState = gameBrain.GetSerializedGameState(),
        IsMoveInProcess = gameBrain.MoveInProcess
    });
    gameBrain = new CheckersBrain(gameOptions, gameStateRepository.GetLastState(gameInfo));
    if (gameBrain.ReachedToEndGame())
    {
        mainMenu.Title = "> Game is over <";
        mainMenu.RunMethod();
    }
    else
    {
        UI.DrawGameBoard(gameBrain.GetBoard(), dictionaryForDestinationAndForDelete);
        Console.WriteLine(gameRepository.GetGameTurn(gameBrain, gameInfo));
        gameMenu._brain = gameBrain;
        gameMenu._game = gameInfo;
        if (dictionaryForDestinationAndForDelete.Count > 0)
        {
            MakeAMove();
        }

        selectedCheckerCoords = null!;
    }
    return "-";
}


string DoNewGame()
{
    Console.WriteLine("Enter name for a game: ");
    gameInfo.Name = Console.ReadLine();
    Console.WriteLine("Enter name for player 1: ");
    string? name = Console.ReadLine();
    gameInfo.Player1Name = name!;
    Console.WriteLine("Enter name for player 2: ");
    name = Console.ReadLine()!;
    gameInfo.Player2Name = name;
    Console.WriteLine("Enter type of player 1 [H] => Human , [B] => Bot: ");
    name = Console.ReadLine()!.ToUpper().Trim();
    while(!name.Equals("H") && !name.Equals("B"))
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid letter, enter [H] for Human and [B] for Bot again: ");
        name = Console.ReadLine()!.ToUpper().Trim();
    }
    if (name.Equals("H"))
    {
        gameInfo.Player1Type = EPlayerType.Human;
    }
    if (name.Equals("B"))
    {
        gameInfo.Player1Type = EPlayerType.AI;
    }
    Console.WriteLine("Enter type of player 2 [H] => Human , [B] => Bot: ");
    name = Console.ReadLine()!.ToUpper().Trim();
    while(!name.Equals("H") && !name.Equals("B"))
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid letter, enter [H] for Human and [B] for Bot again: ");
        name = Console.ReadLine()!.ToUpper().Trim();
    }
    if (name.Equals("H"))
    {
        gameInfo.Player2Type = EPlayerType.Human;
    }
    if (name.Equals("B"))
    {
        gameInfo.Player2Type = EPlayerType.AI;
    }
    Console.WriteLine("Make option, save and load it [ S ]");
    Console.WriteLine("Show saved options and load from them [ L ]");

    string choice = Console.ReadLine()!;
    while(!choice.Equals("S") && !choice.Equals("L"))
    {
        Console.Clear();
        Console.WriteLine("You entered an invalid letter, enter [S] for make a new option and load it or [L] for loading option: ");
        choice = Console.ReadLine()!.ToUpper().Trim();
    }
    switch (choice.ToUpper())
    {
        case "S":
            string createAndReturnFileName = CreateGameOptions();
            gameOptions = gameOptionsRepository.GetGameOptions(int.Parse(createAndReturnFileName));
            gameInfo.CheckersOption = gameOptions;
            gameRepository.UpdateOrCreateGame(createAndReturnFileName, gameInfo);
            break;
        case "L":
            Console.WriteLine("-----------------------");
            string checkForEmptyList = ListGameOptions();
            if (checkForEmptyList.Equals("S"))
            {
                createAndReturnFileName = CreateGameOptions();
                gameOptions = gameOptionsRepository.GetGameOptions(int.Parse(createAndReturnFileName));
                gameInfo.CheckersOption = gameOptions;
                gameRepository.UpdateOrCreateGame(createAndReturnFileName, gameInfo);
                break;
            }
            Console.WriteLine("-----------------------");
            Console.Write("Insert option number:");
            var optionId = Console.ReadLine();
            gameOptions = gameOptionsRepository.GetGameOptions(Int32.Parse(optionId!));
            gameInfo.CheckersOptionId = gameOptions.Id;
            gameInfo.CheckersOption = gameOptions;
            gameRepository.UpdateOrCreateGame(gameInfo.Id.ToString(), gameInfo);

            break;
    }

    gameBrain = new CheckersBrain(gameOptions, null);
    stateOfBoard.GameBoard = gameBrain.GetBoard(); // this should save board in class
    UI.DrawGameBoard(gameBrain.GetBoard(), null);
    Console.WriteLine(gameRepository.GetGameTurn(gameBrain, gameInfo));
    
        gameMenu._brain = gameBrain;
        gameMenu._game = gameInfo; 
    

    
    gameMenu.RunMethod();
    return "-";
}