
namespace WebAppCheckers.Domain;

public class CheckersGameState
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    //public EGamePiece?[,] GameBoard = default!;

    public string SerializedGameState { get; set; } = default!;

    public CheckersGame? CheckersGame { get; set; }
}