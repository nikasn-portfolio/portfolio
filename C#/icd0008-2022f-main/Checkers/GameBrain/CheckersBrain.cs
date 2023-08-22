using System.Text.Json;
using System.Xml.Schema;
using Domain;
using HelperMethods;

namespace GameBrain;

public class CheckersBrain
{
    private readonly CheckersState _state;

    public bool MoveInProcess { get; set; }

    public CheckersBrain(CheckersOption option, CheckersGameState? state)
    {
        if (state == null)
        {
            _state = new CheckersState();
            InitializeNewGame(option);
        }
        else
        {
            CheckersClassStateForSerialization deserialize =
                System.Text.Json.JsonSerializer.Deserialize<CheckersClassStateForSerialization>(
                    state.SerializedGameState)!;
            _state = new CheckersState()
            {
                GameBoard = HelperMethods.JsonConverterForBoard.To2D(deserialize.GameBoard),
                NextMoveByBlack = deserialize.NextMoveByBlack,
                LastMoveByKing = deserialize.LastMoveByKing
            };
        }
    }

    private void InitializeNewGame(CheckersOption option)
    {
        var boardWidth = option.Width;
        var boardHeight = option.Height;

        if (boardWidth < 4 || boardHeight < 4)
        {
            throw new ArgumentException("Board size too small");
        }

        _state.GameBoard = new EGamePiece?[boardWidth, boardHeight];
        var boardHeightLimitForBlackCheckers = ((boardHeight / 2) - 1);
        // for to place black guys!
        for (int i = 0; i < boardHeightLimitForBlackCheckers; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if (j % 2 == 0 && i % 2 != 0)
                {
                    _state.GameBoard[j, i] = EGamePiece.Black;
                }

                if (j % 2 != 0 && i % 2 == 0)
                {
                    _state.GameBoard[j, i] = EGamePiece.Black;
                }
            }
        }

        // for to place white guys!
        for (int i = boardHeight - 1; i > boardHeightLimitForBlackCheckers + 1; i--)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if (j % 2 == 0 && i % 2 != 0)
                {
                    _state.GameBoard[j, i] = EGamePiece.White;
                }

                if (j % 2 != 0 && i % 2 == 0)
                {
                    _state.GameBoard[j, i] = EGamePiece.White;
                }
            }
        }

        _state.NextMoveByBlack = !option.WhiteStarts;
    }

    public string GetSerializedGameState()
    {
        var serializedGameStateClass = new CheckersClassStateForSerialization()
        {
            GameBoard = HelperMethods.JsonConverterForBoard.ToJaggedArray(_state.GameBoard),
            NextMoveByBlack = _state.NextMoveByBlack,
            LastMoveByKing = _state.LastMoveByKing
        };
        return JsonSerializer.Serialize(serializedGameStateClass);
    }

    public EGamePiece?[,] GetBoard()
    {
        var res = new EGamePiece?[_state.GameBoard.GetLength(0), _state.GameBoard.GetLength(1)];
        Array.Copy(_state.GameBoard, res, _state.GameBoard.Length);
        return res;
    }

    public bool NextMoveByBlack()
    {
        return _state.NextMoveByBlack;
    }

    //check around the piece if there is a piece to jump over
    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? FindBlackCheckersToCaptureFromAllSides(
        int boardHeightX, int boardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        // if selected piece is white
        if (_state.GameBoard[boardWidthY, boardHeightX] != EGamePiece.White)
            return dictionaryForDestinationAndForDelete;
        //check if there is a black piece from front
        if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
        {
            if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.Black ||
                _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.BlackKing)
            {
                if (boardHeightX - 2 >= 0 && boardWidthY - 2 >= 0)
                {
                    if (_state.GameBoard[boardWidthY - 2, boardHeightX - 2] == null)
                    {
                        dictionaryForDestinationAndForDelete.Add(
                            new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY - 2),
                            new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1));
                    }
                }
            }
        }

        if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
        {
            if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.Black ||
                _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.BlackKing)
            {
                if (boardHeightX - 2 >= 0 && boardWidthY + 2 < _state.GameBoard.GetLength(0))
                {
                    if (_state.GameBoard[boardWidthY + 2, boardHeightX - 2] == null)
                    {
                        dictionaryForDestinationAndForDelete.Add(
                            new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY + 2),
                            new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1));
                    }
                }
            }
        }

        //check if there is a black piece from back
        if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY - 1 >= 0)
        {
            if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.Black ||
                _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.BlackKing)
            {
                if (boardHeightX + 2 < _state.GameBoard.GetLength(1) && boardWidthY - 2 >= 0)
                {
                    if (_state.GameBoard[boardWidthY - 2, boardHeightX + 2] == null)
                    {
                        dictionaryForDestinationAndForDelete.Add(
                            new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY - 2),
                            new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1));
                    }
                }
            }
        }

        if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY + 1 < _state.GameBoard.GetLength(1))
        {
            if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.Black ||
                _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.BlackKing)
            {
                if (boardHeightX + 2 < _state.GameBoard.GetLength(1) && boardWidthY + 2 < _state.GameBoard.GetLength(0))
                {
                    if (_state.GameBoard[boardWidthY + 2, boardHeightX + 2] == null)
                    {
                        dictionaryForDestinationAndForDelete.Add(
                            new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY + 2),
                            new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1));
                    }
                }
            }
        }


        return dictionaryForDestinationAndForDelete;
    }

    // check if there is a black piece to capture
    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? FindWhiteCheckersToCaptureFromAllSides(
        int boardHeightX, int boardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        // if selected piece is black
        if (_state.GameBoard[boardWidthY, boardHeightX] == EGamePiece.Black)
        {
            //check if there is a black piece around from front
            if (boardWidthY - 1 >= 0 && boardHeightX + 1 < _state.GameBoard.GetLength(1))
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                {
                    if (boardWidthY - 2 >= 0 && boardHeightX + 2 < _state.GameBoard.GetLength(1))
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX + 2] == null)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY - 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1));
                        }
                    }
                }
            }

            if (boardWidthY + 1 < _state.GameBoard.GetLength(0) && boardHeightX + 1 < _state.GameBoard.GetLength(1))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                {
                    if (boardWidthY + 2 < _state.GameBoard.GetLength(0) &&
                        boardHeightX + 2 < _state.GameBoard.GetLength(1))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX + 2] == null)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1));
                        }
                    }
                }
            }

            //check if for a place to jump from back
            if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX - 2 >= 0 && boardWidthY - 2 >= 0)
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX - 2] == null)
                        {
                            dictionaryForDestinationAndForDelete
                                .Add(new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY - 2),
                                    new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1));
                        }
                    }
                }
            }

            if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX - 2 >= 0 && boardWidthY + 2 < _state.GameBoard.GetLength(0))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX - 2] == null)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1));
                        }
                    }
                }
            }
        }


        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? FindAllRegularPossibleMovesForKings(
        int boardHeightX, int boardWidthY, int selectedX, int selectedY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == null)
                {
                    dictionaryForDestinationAndForDelete.Add(
                        new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1),
                        new BoardCoordsForDictionary(selectedX, selectedY));
                }

                if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.BlackKing)
                {
                    break;
                }
            }

            boardHeightX++;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;


        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == null)
                {
                    dictionaryForDestinationAndForDelete.Add(
                        new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1),
                        new BoardCoordsForDictionary(selectedX, selectedY));
                }

                if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.BlackKing)
                {
                    break;
                }
            }

            boardHeightX++;
            boardWidthY--;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == null)
                {
                    dictionaryForDestinationAndForDelete.Add(
                        new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1),
                        new BoardCoordsForDictionary(selectedX, selectedY));
                }

                if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.BlackKing)
                {
                    break;
                }
            }

            boardHeightX--;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == null)
                {
                    dictionaryForDestinationAndForDelete.Add(
                        new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1),
                        new BoardCoordsForDictionary(selectedX, selectedY));
                }

                if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.BlackKing)
                {
                    break;
                }
            }

            boardHeightX--;
            boardWidthY--;
        }

        return dictionaryForDestinationAndForDelete;
    }


    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
        FindAllPossibleWhiteCheckersToCaptureByBlackKing(int boardHeightX, int boardWidthY, int selectedX,
            int selectedY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.Black)
                {
                    if (boardHeightX + 2 < _state.GameBoard.GetLength(1) &&
                        boardWidthY + 2 < _state.GameBoard.GetLength(0))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX + 2] == null &&
                            _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.White ||
                            _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX++;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.BlackKing)
                {
                    if (boardHeightX + 2 < _state.GameBoard.GetLength(1) && boardWidthY - 2 >= 0)
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX + 2] == null &&
                            _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.White ||
                            _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY - 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX++;
            boardWidthY--;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.Black)
                {
                    if (boardHeightX - 2 >= 0 &&
                        boardWidthY + 2 < _state.GameBoard.GetLength(0))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX - 2] == null &&
                            _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.White ||
                            _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX--;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.WhiteKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.Black)
                {
                    if (boardHeightX - 2 >= 0 && boardWidthY - 2 >= 0)
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX - 2] == null &&
                            _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.White ||
                            _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY - 2),
                                new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX--;
            boardWidthY--;
        }


        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
        FindAllPossibleBlackCheckersToCaptureByWhiteKing(int boardHeightX, int boardWidthY, int selectedX,
            int selectedY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX - 2 >= 0 &&
                        boardWidthY + 2 < _state.GameBoard.GetLength(0))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX - 2] == null &&
                            _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.Black ||
                            _state.GameBoard[boardWidthY + 1, boardHeightX - 1] == EGamePiece.BlackKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX--;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX - 2 >= 0 && boardWidthY - 2 >= 0)
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX - 2] == null &&
                            _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.Black ||
                            _state.GameBoard[boardWidthY - 1, boardHeightX - 1] == EGamePiece.BlackKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX - 2, boardWidthY - 2),
                                new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX--;
            boardWidthY--;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY + 1 < _state.GameBoard.GetLength(0))
            {
                if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX + 2 < _state.GameBoard.GetLength(1) &&
                        boardWidthY + 2 < _state.GameBoard.GetLength(0))
                    {
                        if (_state.GameBoard[boardWidthY + 2, boardHeightX + 2] == null &&
                            _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.Black ||
                            _state.GameBoard[boardWidthY + 1, boardHeightX + 1] == EGamePiece.BlackKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY + 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX++;
            boardWidthY++;
        }

        boardHeightX = selectedX;
        boardWidthY = selectedY;

        while (boardHeightX >= 0 && boardHeightX < _state.GameBoard.GetLength(1) &&
               boardWidthY < _state.GameBoard.GetLength(0) && boardWidthY >= 0)
        {
            if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY - 1 >= 0)
            {
                if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.Black ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.BlackKing ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.White ||
                    _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.WhiteKing)
                {
                    if (boardHeightX + 2 < _state.GameBoard.GetLength(1) &&
                        boardWidthY - 2 >= 0)
                    {
                        if (_state.GameBoard[boardWidthY - 2, boardHeightX + 2] == null &&
                            _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.Black ||
                            _state.GameBoard[boardWidthY - 1, boardHeightX + 1] == EGamePiece.BlackKing)
                        {
                            dictionaryForDestinationAndForDelete.Add(
                                new BoardCoordsForDictionary(boardHeightX + 2, boardWidthY - 2),
                                new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1));
                        }

                        break;
                    }
                }
            }

            boardHeightX++;
            boardWidthY--;
        }

        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
        FindPossibleRegularMovesForBlackSelectedChecker(int boardHeightX, int boardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY - 1 >= 0)
        {
            if (_state.GameBoard[boardWidthY - 1, boardHeightX + 1] == null)
            {
                dictionaryForDestinationAndForDelete.Add(
                    new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY - 1),
                    new BoardCoordsForDictionary(boardHeightX, boardWidthY));
            }
        }

        if (boardHeightX + 1 < _state.GameBoard.GetLength(1) && boardWidthY + 1 < _state.GameBoard.GetLength(0))
        {
            if (_state.GameBoard[boardWidthY + 1, boardHeightX + 1] == null)
            {
                dictionaryForDestinationAndForDelete.Add(
                    new BoardCoordsForDictionary(boardHeightX + 1, boardWidthY + 1),
                    new BoardCoordsForDictionary(boardHeightX, boardWidthY));
            }
        }

        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
        FindPossibleRegularMovesForWhiteSelectedChecker(int boardHeightX, int boardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        if (boardHeightX - 1 >= 0 && boardWidthY - 1 >= 0)
        {
            if (_state.GameBoard[boardWidthY - 1, boardHeightX - 1] == null)
            {
                dictionaryForDestinationAndForDelete.Add(
                    new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY - 1),
                    new BoardCoordsForDictionary(boardHeightX, boardWidthY));
            }
        }

        if (boardHeightX - 1 >= 0 && boardWidthY + 1 < _state.GameBoard.GetLength(0))
        {
            if (_state.GameBoard[boardWidthY + 1, boardHeightX - 1] == null)
            {
                dictionaryForDestinationAndForDelete.Add(
                    new BoardCoordsForDictionary(boardHeightX - 1, boardWidthY + 1),
                    new BoardCoordsForDictionary(boardHeightX, boardWidthY));
            }
        }

        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? MakeMove(int selectedBoardHeightX,
        int selectedBoardWidthY, int desBoardHeightX, int desBoardWidthY, int delBoardHeightX, int delBoardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete;
        if (_state.NextMoveByBlack)
        {
            dictionaryForDestinationAndForDelete = MakeMoveBlack(selectedBoardHeightX, selectedBoardWidthY,
                desBoardHeightX, desBoardWidthY, delBoardHeightX, delBoardWidthY);
        }
        else
        {
            dictionaryForDestinationAndForDelete = MakeMoveWhite(selectedBoardHeightX, selectedBoardWidthY,
                desBoardHeightX, desBoardWidthY, delBoardHeightX, delBoardWidthY);
        }


        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? MakeMoveBlack(int selectedBoardHeightX,
        int selectedBoardWidthY, int desBoardHeightX, int desBoardWidthY, int delBoardHeightX, int delBoardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();

        // move for simple black checker without capturing
        if (selectedBoardHeightX == delBoardHeightX && selectedBoardWidthY == delBoardWidthY &&
            (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.Black ||
             _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.BlackKing))
        {
            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.Black)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.Black;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                if (desBoardHeightX == _state.GameBoard.GetLength(1) - 1 &&
                    _state.GameBoard[desBoardWidthY, desBoardHeightX] == EGamePiece.Black)
                {
                    _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.BlackKing;
                }
            }
            else if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.BlackKing)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.BlackKing;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
            }
        }
        else if ((_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.Black ||
                  _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.BlackKing) &&
                 selectedBoardHeightX != delBoardHeightX && selectedBoardWidthY != delBoardWidthY)
        {
            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.BlackKing)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.BlackKing;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                _state.GameBoard[delBoardWidthY, delBoardHeightX] = null;
                dictionaryForDestinationAndForDelete =
                    FindAllPossibleWhiteCheckersToCaptureByBlackKing(desBoardHeightX, desBoardWidthY, desBoardHeightX,
                        desBoardWidthY);
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> dictionaryOfMovesAfterCapture =
                    new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
                if (dictionaryForDestinationAndForDelete!.Count > 0)
                {
                    var dicCount = dictionaryForDestinationAndForDelete.Count;
                    while (dicCount != 0)
                    {
                        int changeByX = dictionaryForDestinationAndForDelete.Keys.First().BoardHeightX -
                                        dictionaryForDestinationAndForDelete.Values.First().BoardHeightX;
                        int changeByY = dictionaryForDestinationAndForDelete.Keys.First().BoardWidthY -
                                        dictionaryForDestinationAndForDelete.Values.First().BoardWidthY;
                        int startedX = dictionaryForDestinationAndForDelete.Values.First().BoardHeightX;
                        int startedY = dictionaryForDestinationAndForDelete.Values.First().BoardWidthY;
                        dictionaryOfMovesAfterCapture = FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                            changeByY, startedX, startedY, startedX, startedY);
                        dictionaryForDestinationAndForDelete.Remove(dictionaryForDestinationAndForDelete.Keys.First());
                        dicCount--;
                        dictionaryForDestinationAndForDelete = dictionaryForDestinationAndForDelete
                            .Concat(dictionaryOfMovesAfterCapture).ToDictionary(e => e.Key, e => e.Value);
                    }
                }

                MoveInProcess = true;
            }

            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.Black)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.Black;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                _state.GameBoard[delBoardWidthY, delBoardHeightX] = null;
                dictionaryForDestinationAndForDelete =
                    FindWhiteCheckersToCaptureFromAllSides(desBoardHeightX, desBoardWidthY);
                MoveInProcess = true;
            }

            // if during the move checker becomes a king it checks for possible moves for king
            if (desBoardHeightX == _state.GameBoard.GetLength(1) - 1 &&
                _state.GameBoard[desBoardWidthY, desBoardHeightX] == EGamePiece.Black)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.BlackKing;
                dictionaryForDestinationAndForDelete =
                    FindAllPossibleWhiteCheckersToCaptureByBlackKing(desBoardHeightX, desBoardWidthY, desBoardHeightX,
                        desBoardWidthY);
                MoveInProcess = true;
            }


            if (dictionaryForDestinationAndForDelete!.Count > 0)
            {
                return dictionaryForDestinationAndForDelete;
            }
        }

        _state.NextMoveByBlack = !_state.NextMoveByBlack;
        MoveInProcess = false;

        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? MakeMoveWhite(int selectedBoardHeightX,
        int selectedBoardWidthY, int desBoardHeightX, int desBoardWidthY, int delBoardHeightX, int delBoardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        if (selectedBoardHeightX == delBoardHeightX && selectedBoardWidthY == delBoardWidthY &&
            (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.White ||
             _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.WhiteKing))
        {
            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.White)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.White;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                if (desBoardHeightX == 0 && _state.GameBoard[desBoardWidthY, desBoardHeightX] == EGamePiece.White)
                {
                    _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.WhiteKing;
                }

                MoveInProcess = false;
            }

            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.WhiteKing)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.WhiteKing;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                MoveInProcess = false;
            }
        }
        else if ((_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.White ||
                  _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.WhiteKing) &&
                 selectedBoardHeightX != delBoardHeightX && selectedBoardWidthY != delBoardWidthY)
        {
            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.WhiteKing)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.WhiteKing;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                _state.GameBoard[delBoardWidthY, delBoardHeightX] = null;
                dictionaryForDestinationAndForDelete =
                    FindAllPossibleBlackCheckersToCaptureByWhiteKing(desBoardHeightX, desBoardWidthY, desBoardHeightX,
                        desBoardWidthY);
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> dictionaryOfMovesAfterCapture =
                    new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
                if (dictionaryForDestinationAndForDelete!.Count > 0)
                {
                    var dicCount = dictionaryForDestinationAndForDelete.Count;
                    while (dicCount != 0)
                    {
                        int changeByX = dictionaryForDestinationAndForDelete.Keys.First().BoardHeightX -
                                        dictionaryForDestinationAndForDelete.Values.First().BoardHeightX;
                        int changeByY = dictionaryForDestinationAndForDelete.Keys.First().BoardWidthY -
                                        dictionaryForDestinationAndForDelete.Values.First().BoardWidthY;
                        int startedX = dictionaryForDestinationAndForDelete.Values.First().BoardHeightX;
                        int startedY = dictionaryForDestinationAndForDelete.Values.First().BoardWidthY;
                        dictionaryOfMovesAfterCapture = FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                            changeByY, startedX, startedY, startedX, startedY);
                        dictionaryForDestinationAndForDelete.Remove(dictionaryForDestinationAndForDelete.Keys.First());
                        dicCount--;
                        dictionaryForDestinationAndForDelete = dictionaryForDestinationAndForDelete
                            .Concat(dictionaryOfMovesAfterCapture).ToDictionary(e => e.Key, e => e.Value);
                    }
                }

                MoveInProcess = true;
            }

            if (_state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] == EGamePiece.White)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.White;
                _state.GameBoard[selectedBoardWidthY, selectedBoardHeightX] = null;
                _state.GameBoard[delBoardWidthY, delBoardHeightX] = null;
                dictionaryForDestinationAndForDelete =
                    FindBlackCheckersToCaptureFromAllSides(desBoardHeightX, desBoardWidthY);
                MoveInProcess = true;
            }

            if (desBoardHeightX == 0 &&
                _state.GameBoard[desBoardWidthY, desBoardHeightX] == EGamePiece.White)
            {
                _state.GameBoard[desBoardWidthY, desBoardHeightX] = EGamePiece.WhiteKing;
                dictionaryForDestinationAndForDelete =
                    FindAllPossibleWhiteCheckersToCaptureByBlackKing(desBoardHeightX, desBoardWidthY, desBoardHeightX,
                        desBoardWidthY);
            }
        }

        if (dictionaryForDestinationAndForDelete!.Count > 0)
        {
            return dictionaryForDestinationAndForDelete;
        }

        MoveInProcess = false;

        _state.NextMoveByBlack = !_state.NextMoveByBlack;

        return dictionaryForDestinationAndForDelete;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>
        FindAllRegularPossibleMovesForKingsInCertainDirection(int changeByX, int changeByY, int desBoardHeightX,
            int desBoardWidthY, int selectedBoardHeightX, int selectedBoardWidthY)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary> dictionaryForDestinationAndForDelete =
            new Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>();
        while (desBoardHeightX + changeByX >= 0 && desBoardHeightX + changeByX < _state.GameBoard.GetLength(1) &&
               desBoardWidthY + changeByY < _state.GameBoard.GetLength(0) && desBoardWidthY + changeByY >= 0)
        {
            if (_state.GameBoard[desBoardWidthY + changeByY, desBoardHeightX + changeByX] == null)
            {
                dictionaryForDestinationAndForDelete.Add(
                    new BoardCoordsForDictionary(desBoardHeightX + changeByX
                        , desBoardWidthY + changeByY),
                    new BoardCoordsForDictionary(selectedBoardHeightX, selectedBoardWidthY));
            }
            else
            {
                break;
            }

            desBoardHeightX += changeByX;
            desBoardWidthY += changeByY;
        }

        return dictionaryForDestinationAndForDelete;
    }


    // make a move
    public EGamePiece? ReturnsPiece(int x, int y)
    {
        return _state.GameBoard[y, x];
    }

    public bool LastMoveByKing()
    {
        return _state.LastMoveByKing;
    }

    public bool SwitchLastMoveByKing()
    {
        return _state.LastMoveByKing = !_state.LastMoveByKing;
    }

    public void SwitchNextMoveByBlack()
    {
        _state.NextMoveByBlack = !_state.NextMoveByBlack;
    }

    public BoardCoordsForDictionary SelectedCoords(int x, int y)
    {
        return new BoardCoordsForDictionary(x, y);
    }

    public bool ReachedToEndGame()
    {
        List<BoardCoordsForDictionary> ListOfBlack = new List<BoardCoordsForDictionary>();
        List<BoardCoordsForDictionary> ListOfWhite = new List<BoardCoordsForDictionary>();

        for (int px = 0; px < _state.GameBoard.GetLength(1); px++)
        {
            for (int py = 0; py < _state.GameBoard.GetLength(0); py++)
            {
                if (ReturnsPiece(px, py).Equals(EGamePiece.Black) || ReturnsPiece(px, py).Equals(EGamePiece.BlackKing))
                {
                    ListOfBlack.Add(new BoardCoordsForDictionary(px, py));
                }
                else if (ReturnsPiece(px, py).Equals(EGamePiece.White) ||
                         ReturnsPiece(px, py).Equals(EGamePiece.WhiteKing))
                {
                    ListOfWhite.Add(new BoardCoordsForDictionary(px, py));
                }
            }
        }

        if (ListOfWhite.Count == 0 || ListOfBlack.Count == 0)
        {
            return true;
        }

        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dicForCheckForMoves;
        if (NextMoveByBlack())
        {
            foreach (var checker in ListOfBlack)
            {
                dicForCheckForMoves = FindPossibleMovesForSelectedChecker(checker.BoardHeightX, checker.BoardWidthY);
                if (dicForCheckForMoves is { Count: > 0 })
                {
                    return false;
                }
            }
        }

        if (NextMoveByBlack() == false)
        {
            foreach (var checker in ListOfWhite)
            {
                dicForCheckForMoves = FindPossibleMovesForSelectedChecker(checker.BoardHeightX, checker.BoardWidthY);
                if (dicForCheckForMoves is { Count: > 0 })
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? FindPossibleMovesForSelectedChecker(int i,
        int i1)
    {
        Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? boardCoordsForDictionaries = null;
        if (ReturnsPiece(i, i1) == EGamePiece.Black && NextMoveByBlack())
        {
            boardCoordsForDictionaries = FindWhiteCheckersToCaptureFromAllSides(i, i1);
            if (boardCoordsForDictionaries!.Count == 0)
            {
                boardCoordsForDictionaries =
                    FindPossibleRegularMovesForBlackSelectedChecker(i, i1);
            }
            else
            {
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForSimpleMovesAndDelete =
                    FindPossibleRegularMovesForBlackSelectedChecker(i, i1);

                if (dictionaryForSimpleMovesAndDelete!.Count > 0)
                {
                    boardCoordsForDictionaries = boardCoordsForDictionaries
                        .Concat(dictionaryForSimpleMovesAndDelete).ToDictionary(e => e.Key, e => e.Value);
                }
            }
        }
        else if (ReturnsPiece(i, i1) == EGamePiece.BlackKing && NextMoveByBlack())
        {
            boardCoordsForDictionaries =
                FindAllPossibleWhiteCheckersToCaptureByBlackKing(i, i1, i, i1);
            int dicCount = 0;
            if (boardCoordsForDictionaries != null)
            {
                dicCount = boardCoordsForDictionaries.Count;
            }

            if (boardCoordsForDictionaries!.Count == 0)
            {
                boardCoordsForDictionaries =
                    FindAllRegularPossibleMovesForKings(i, i1, i, i1);
            }
            else
            {
                while (dicCount != 0)
                {
                    Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
                        dictionaryForSimpleMovesAndDelete =
                            FindAllRegularPossibleMovesForKings(i, i1, i, i1);
                    int changeByX = boardCoordsForDictionaries.Keys.First().BoardHeightX -
                                    boardCoordsForDictionaries.Values.First().BoardHeightX;
                    int changeByY = boardCoordsForDictionaries.Keys.First().BoardWidthY -
                                    boardCoordsForDictionaries.Values.First().BoardWidthY;
                    int selX = boardCoordsForDictionaries.Values.First().BoardHeightX;
                    int selY = boardCoordsForDictionaries.Values.First().BoardWidthY;
                    if (dictionaryForSimpleMovesAndDelete!.Count > 0)
                    {
                        boardCoordsForDictionaries.Remove(
                            boardCoordsForDictionaries.Keys.First());
                        dicCount--;
                        boardCoordsForDictionaries = boardCoordsForDictionaries
                            .Concat(dictionaryForSimpleMovesAndDelete)
                            .Concat(FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }
                    else
                    {
                        dicCount--;
                        boardCoordsForDictionaries.Clear();
                        boardCoordsForDictionaries = boardCoordsForDictionaries
                            .Concat(FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }
                }
            }
        }
        else if (ReturnsPiece(i, i1) == EGamePiece.White && NextMoveByBlack() == false)
        {
            boardCoordsForDictionaries = FindBlackCheckersToCaptureFromAllSides(i, i1);
            if (boardCoordsForDictionaries!.Count == 0)
            {
                boardCoordsForDictionaries =
                    FindPossibleRegularMovesForWhiteSelectedChecker(i, i1);
            }
            else
            {
                Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dictionaryForSimpleMovesAndDelete =
                    FindPossibleRegularMovesForWhiteSelectedChecker(i, i1);

                if (dictionaryForSimpleMovesAndDelete!.Count > 0)
                {
                    boardCoordsForDictionaries = boardCoordsForDictionaries
                        .Concat(dictionaryForSimpleMovesAndDelete).ToDictionary(e => e.Key, e => e.Value);
                }
            }
        }
        else if (ReturnsPiece(i, i1) == EGamePiece.WhiteKing && NextMoveByBlack() == false)
        {
            boardCoordsForDictionaries =
                FindAllPossibleBlackCheckersToCaptureByWhiteKing(i, i1, i, i1);
            int dicCount = 0;
            if (boardCoordsForDictionaries != null)
            {
                dicCount = boardCoordsForDictionaries.Count;
            }
            if (boardCoordsForDictionaries!.Count == 0)
            {
                boardCoordsForDictionaries =
                    FindAllRegularPossibleMovesForKings(i, i1, i, i1);
            }
            else
            {
                while (dicCount != 0)
                {
                    Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>?
                        dictionaryForSimpleMovesAndDelete =
                            FindAllRegularPossibleMovesForKings(i, i1, i, i1);
                    int changeByX = boardCoordsForDictionaries.Keys.First().BoardHeightX -
                                    boardCoordsForDictionaries.Values.First().BoardHeightX;
                    int changeByY = boardCoordsForDictionaries.Keys.First().BoardWidthY -
                                    boardCoordsForDictionaries.Values.First().BoardWidthY;
                    int selX = boardCoordsForDictionaries.Values.First().BoardHeightX;
                    int selY = boardCoordsForDictionaries.Values.First().BoardWidthY;
                    if (dictionaryForSimpleMovesAndDelete!.Count > 0)
                    {
                        boardCoordsForDictionaries.Remove(
                            boardCoordsForDictionaries.Keys.First());
                        dicCount--;
                        boardCoordsForDictionaries = boardCoordsForDictionaries
                            .Concat(dictionaryForSimpleMovesAndDelete)
                            .Concat(FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }
                    else
                    {
                        dicCount--;
                        boardCoordsForDictionaries.Clear();
                        boardCoordsForDictionaries = boardCoordsForDictionaries
                            .Concat(FindAllRegularPossibleMovesForKingsInCertainDirection(changeByX,
                                changeByY,
                                selX, selY, selX, selY)).ToDictionary(e => e.Key, e => e.Value);
                    }
                }
            }
        }

        return boardCoordsForDictionaries;
    }
}