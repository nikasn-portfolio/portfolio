using Domain;

namespace ConsoleUI;

public static class UI

{
    public static void DrawGameBoard(EGamePiece?[,] board, Dictionary<BoardCoordsForDictionary, BoardCoordsForDictionary>? dic)
    {
        var cols = board.GetLength(0);
        var rows = board.GetLength(1);
        Console.WriteLine();
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"    {j}");
        }
        Console.WriteLine();
        for (int i = 0; i < rows; i++)
        {
            Console.Write("  ");
            for (int j = 0; j < cols; j++)
            {
                Console.Write("+----");
            }

            Console.WriteLine("+");

            for (int j = 0; j < cols; j++)
            {
                if (j == 0)
                {
                    Console.Write($"{i}");
                }
                Console.Write("| ");
                var pieceStr = "  ";
                if (board[j, i] == EGamePiece.Black)
                {
                    pieceStr = " B";
                }else if (board[j, i] == EGamePiece.White)
                {
                    pieceStr = " W";
                }else if (board[j, i] == EGamePiece.WhiteKing)
                {
                    pieceStr = "WK";
                }else if (board[j, i] == EGamePiece.BlackKing)
                {
                    pieceStr = "BK";
                }
                
                if (dic != null && dic.Count > 0)
                {
                    foreach (var coords in dic)
                    {
                        if (coords.Key.BoardWidthY == j && coords.Key.BoardHeightX == i)
                        {
                            pieceStr = "MV";
                        }
                    }
                }

                Console.Write(pieceStr);
                Console.Write(" ");
            }

            Console.WriteLine("|");
        }
        Console.Write("  ");
        for (int j = 0; j < cols; j++)
        {
            Console.Write("+----");
        }

        Console.WriteLine("+");

/*
    A    B
  +---+---+
2 | X | O |
  +---+---+
1 | O | X |
  +---+---+




 */
        }
    }
