using Domain;
using GameBrain;

namespace MenuSystem;

public class Menu
{
    public string Title { get; set; }
    public string? NextMoveBy { get; set; }
    private readonly EMenuLevel _level;
    public CheckersBrain _brain;
    public CheckersGame _game;
    private const string ShortcutExit = "X";
    private const string ShortcutGoBack = "B";
    private const string ShortcutGoToMain = "M";
    private readonly Dictionary<string, MenuItem> _menuItems = new Dictionary<string, MenuItem>();
    private readonly MenuItem _menuItemExit = new MenuItem(ShortcutExit, "Exit", null);
    private readonly MenuItem _menuItemGoBack = new MenuItem(ShortcutGoBack, "Back", null);
    private readonly MenuItem _menuItemGoToMain = new MenuItem(ShortcutGoToMain, "Main menu", null);

    public Menu(EMenuLevel level, string title, string? nextMoveBy, List<MenuItem> menuItems, CheckersBrain? brain, CheckersGame? game)
    {
        _brain = brain!;
        _game = game!;
        _level = level;
        Title = title;
        NextMoveBy = nextMoveBy;
        foreach (var menuItem in menuItems)
        {
            _menuItems.Add(menuItem.ShortCut, menuItem);
        }

        if (_level != EMenuLevel.Main)
            _menuItems.Add(ShortcutGoBack, _menuItemGoBack);

        if (_level == EMenuLevel.Other)
            _menuItems.Add(ShortcutGoToMain, _menuItemGoToMain);

        _menuItems.Add(ShortcutExit, _menuItemExit);
    }

    public string RunMethod()
    {
        var menuDone = false;
        var userChoice = "";
        do
        {
            Console.WriteLine(Title);
            Console.WriteLine(NextMoveBy);
            Console.WriteLine("===================");
            foreach (var menuItem in _menuItems.Values)
            {
                Console.WriteLine(menuItem);
            }


            Console.WriteLine("-------------------");
            Console.Write("Your choice:");
            if (_brain != null && _game != null &&
                (_game.Player2Type == EPlayerType.AI && _brain.NextMoveByBlack() == false ||
                 _game.Player1Type == EPlayerType.AI && _brain.NextMoveByBlack()))
            {
                userChoice = "P";
            }
            else
            {
                userChoice = Console.ReadLine()?.ToUpper().Trim() ?? "";

            }
            
            if (_brain != null && _brain.ReachedToEndGame())
            {
                userChoice = ShortcutExit;
            }

            

            if (_menuItems.ContainsKey(userChoice))
            {
                string? runReturnValue = null;
                if (_menuItems[userChoice].MethodToRun != null)
                {
                    runReturnValue = _menuItems[userChoice].MethodToRun!();
                }

                if (userChoice == ShortcutGoBack)
                {
                    menuDone = true;
                }

                if (
                    runReturnValue == ShortcutExit ||
                    userChoice == ShortcutExit
                )
                {
                    userChoice = runReturnValue ?? userChoice;
                    menuDone = true;
                }

                if ((userChoice == ShortcutGoToMain || runReturnValue == ShortcutGoToMain) &&
                    _level != EMenuLevel.Main)
                {
                    userChoice = runReturnValue ?? userChoice;
                    menuDone = true;
                }
            }
        } while (!menuDone);

        return userChoice;
    }
}