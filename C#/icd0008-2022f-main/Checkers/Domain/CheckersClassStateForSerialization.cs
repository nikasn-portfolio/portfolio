namespace Domain;

public class CheckersClassStateForSerialization
{
    public EGamePiece?[][] GameBoard { get; set; } = default!;
    public bool NextMoveByBlack { get; set; }

    public bool LastMoveByKing { get; set; } = false;

    public DateTime CreatedAt { get; set; } = default!;
}