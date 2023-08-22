using System.ComponentModel.DataAnnotations;
namespace WebAppCheckers.Domain;

public class CheckersGame
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.Now;
    public DateTime? EndedAt { get; set; }
    public string? GameWonPlayer { get; set; }
    
    [MaxLength(128)]
    public string Player1Name { get; set; } = default!;
    public EPlayerType Player1Type { get; set; }
    
    [MaxLength(128)]
    public string Player2Name { get; set; } = default!;
    public EPlayerType Player2Type { get; set; }

    public int CheckersOptionId { get; set; }
    public CheckersOption? CheckersOption { get; set; }

    public ICollection<WebAppCheckers.Domain.CheckersGameState>? CheckersGameStates { get; set; }
}