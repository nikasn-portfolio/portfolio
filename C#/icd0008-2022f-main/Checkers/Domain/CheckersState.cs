namespace Domain;

public class CheckersState
{
    public EGamePiece?[,] GameBoard { get; set; } = default!;
    public bool NextMoveByBlack { get; set; }

    public bool LastMoveByKing { get; set; } = false;
}