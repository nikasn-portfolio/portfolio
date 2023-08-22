namespace Domain;

public class CheckersGameState
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string SerializedGameState { get; set; } = default!;

    public bool IsMoveInProcess { get; set; } = false;

    public CheckersGame? CheckersGame { get; set; }
}