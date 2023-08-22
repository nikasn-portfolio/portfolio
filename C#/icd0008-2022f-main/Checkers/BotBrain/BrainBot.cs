using System.Runtime.CompilerServices;
using Domain;
using GameBrain;
using Random = System.Random;

namespace BotBrain;

public class BrainBot
{
    public int SelectedX { get; set; } = default!;
    public int SelectedY { get; set; } = default!;
    public BoardCoordsForDictionary? SelectedCheckerCoords { get; set; }
    public List<BoardCoordsForDictionary>? ListOfBlack { get; set; }
    public List<BoardCoordsForDictionary>? ListOfWhite { get; set; }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? DictionaryForDestinationAndDelete
    {
        get;
        set;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? DictionaryForKingsToCapture { get; set; }

    

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? GetCoordsForMove(CheckersBrain gameBrain,
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dic)
    {
        if (dic is { Count: > 0 })
        {
            return dic;
        }

        DictionaryForDestinationAndDelete = new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        SelectedCheckerCoords = null;
        DictionaryForKingsToCapture = new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        ListOfBlack = new List<BoardCoordsForDictionary>();
        ListOfWhite = new List<BoardCoordsForDictionary>();

        for (int px = 0; px < gameBrain.GetBoard().GetLength(1); px++)
        {
            for (int py = 0; py < gameBrain.GetBoard().GetLength(0); py++)
            {
                if ((gameBrain.ReturnsPiece(px, py) == EGamePiece.Black ||
                     gameBrain.ReturnsPiece(px, py) == EGamePiece.BlackKing) && gameBrain.NextMoveByBlack())
                {
                    ListOfBlack.Add(new BoardCoordsForDictionary(px, py));
                }
                else if ((gameBrain.ReturnsPiece(px, py) == EGamePiece.White ||
                          gameBrain.ReturnsPiece(px, py) == EGamePiece.WhiteKing) &&
                         gameBrain.NextMoveByBlack() == false)
                {
                    ListOfWhite.Add(new BoardCoordsForDictionary(px, py));
                }
            }
        }

        if (gameBrain.NextMoveByBlack())
        {
            foreach (var checker in ListOfBlack)
            {
                if (gameBrain.ReturnsPiece(checker.BoardHeightX, checker.BoardWidthY) == EGamePiece.Black)
                {
                    DictionaryForDestinationAndDelete =
                        gameBrain.FindWhiteCheckersToCaptureFromAllSides(checker.BoardHeightX, checker.BoardWidthY);
                    if (DictionaryForDestinationAndDelete!.Count > 0)
                    {
                        SelectedCheckerCoords = checker;
                        DictionaryForKingsToCapture = DictionaryForDestinationAndDelete;
                        break;
                    }
                }

                if (gameBrain.ReturnsPiece(checker.BoardHeightX, checker.BoardWidthY) == EGamePiece.BlackKing)
                {
                    DictionaryForDestinationAndDelete =
                        gameBrain.FindAllPossibleWhiteCheckersToCaptureByBlackKing(checker.BoardHeightX,
                            checker.BoardWidthY, checker.BoardHeightX, checker.BoardWidthY);
                    int dicCount = DictionaryForDestinationAndDelete!.Count;
                    while (dicCount != 0)
                    {
                        int changeByX = DictionaryForDestinationAndDelete.Keys.First().BoardHeightX -
                                        DictionaryForDestinationAndDelete.Values.First().BoardHeightX;
                        int changeByY = DictionaryForDestinationAndDelete.Keys.First().BoardWidthY -
                                        DictionaryForDestinationAndDelete.Values.First().BoardWidthY;
                        int selX = DictionaryForDestinationAndDelete.Values.First().BoardHeightX;
                        int selY = DictionaryForDestinationAndDelete.Values.First().BoardWidthY;

                        DictionaryForDestinationAndDelete.Remove(
                            DictionaryForDestinationAndDelete.Keys.First());
                        dicCount--;
                        DictionaryForDestinationAndDelete = DictionaryForDestinationAndDelete
                            .Concat(gameBrain.FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }

                    if (DictionaryForDestinationAndDelete!.Count > 0)
                    {
                        SelectedCheckerCoords = checker;
                        DictionaryForKingsToCapture = DictionaryForDestinationAndDelete;
                        break;
                    }
                }
            }
        }

        if (gameBrain.NextMoveByBlack() == false)
        {
            foreach (var checker in ListOfWhite)
            {
                if (gameBrain.ReturnsPiece(checker.BoardHeightX, checker.BoardWidthY) == EGamePiece.White)
                {
                    DictionaryForDestinationAndDelete =
                        gameBrain.FindBlackCheckersToCaptureFromAllSides(checker.BoardHeightX, checker.BoardWidthY);
                    if (DictionaryForDestinationAndDelete!.Count > 0)
                    {
                        SelectedCheckerCoords = checker;
                        DictionaryForKingsToCapture = DictionaryForDestinationAndDelete;
                        break;
                    }
                }

                if (gameBrain.ReturnsPiece(checker.BoardHeightX, checker.BoardWidthY) == EGamePiece.WhiteKing)
                {
                    DictionaryForDestinationAndDelete =
                        gameBrain.FindAllPossibleBlackCheckersToCaptureByWhiteKing(checker.BoardHeightX,
                            checker.BoardWidthY, checker.BoardHeightX, checker.BoardWidthY);
                    int dicCount = DictionaryForDestinationAndDelete!.Count;
                    while (dicCount != 0)
                    {
                        int changeByX = DictionaryForDestinationAndDelete.Keys.First().BoardHeightX -
                                        DictionaryForDestinationAndDelete.Values.First().BoardHeightX;
                        int changeByY = DictionaryForDestinationAndDelete.Keys.First().BoardWidthY -
                                        DictionaryForDestinationAndDelete.Values.First().BoardWidthY;
                        int selX = DictionaryForDestinationAndDelete.Values.First().BoardHeightX;
                        int selY = DictionaryForDestinationAndDelete.Values.First().BoardWidthY;

                        DictionaryForDestinationAndDelete.Remove(
                            DictionaryForDestinationAndDelete.Keys.First());
                        dicCount--;
                        DictionaryForDestinationAndDelete = DictionaryForDestinationAndDelete
                            .Concat(gameBrain.FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }

                    if (DictionaryForDestinationAndDelete!.Count > 0)
                    {
                        SelectedCheckerCoords = checker;
                        DictionaryForKingsToCapture = DictionaryForDestinationAndDelete;
                        break;
                    }
                }
            }
        }

        if (SelectedCheckerCoords != null)
        {
            var x = SelectedCheckerCoords.BoardHeightX;
            var y = SelectedCheckerCoords.BoardWidthY;
            DictionaryForDestinationAndDelete = gameBrain.FindPossibleMovesForSelectedChecker(x, y);
        }
        else
        {
            if (DictionaryForDestinationAndDelete == null)
            {
                throw new Exception("DictionaryForDestinationAndDelete is null");
            }
            while (DictionaryForDestinationAndDelete != null && DictionaryForDestinationAndDelete!.Count == 0)
            {
                var rand = new Random();

                if (gameBrain.NextMoveByBlack())
                {
                    SelectedCheckerCoords = ListOfBlack[rand.Next(ListOfBlack.Count)];
                }
                else
                {
                    SelectedCheckerCoords = ListOfWhite[rand.Next(ListOfWhite.Count)];
                }

                var x = SelectedCheckerCoords.BoardHeightX;
                var y = SelectedCheckerCoords.BoardWidthY;
                DictionaryForDestinationAndDelete = gameBrain.FindPossibleMovesForSelectedChecker(x, y);
            }
        }


        return DictionaryForDestinationAndDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> TakeRandomFromDictionary(
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dic)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> result =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        var random = new Random();
        if (DictionaryForKingsToCapture != null && DictionaryForKingsToCapture.Count > 0)
        {
            var key = DictionaryForKingsToCapture.Keys.ElementAt(random.Next(DictionaryForKingsToCapture.Count));
            var value = DictionaryForKingsToCapture[key];
            result.Add(key, value);
            return result;
        }

        int randomIndex = random.Next(0, dic!.Count);
        result.Add(dic.Keys.ElementAt(randomIndex), dic.Values.ElementAt(randomIndex));
        return result;
    }

    public BoardCoordsForDictionary ReturnSelectedCoords(BoardCoordsForDictionary? selectedCoords)
    {
        if (selectedCoords != null)
        {
            return selectedCoords;
        }

        return SelectedCheckerCoords!;
    }
}