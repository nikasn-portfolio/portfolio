namespace Domain;

public class BoardCoordsForDictionary
{
    public int BoardHeightX { get; set; }
    public int BoardWidthY { get; set; }
    public BoardCoordsForDictionary(){}

    public BoardCoordsForDictionary(int boardHeightX, int boardWidthY)
    {
        BoardHeightX = boardHeightX;
        BoardWidthY = boardWidthY;
    }
    
    public int GetBoardHeightX()
    {
        return BoardHeightX;
    }
    
    public int GetBoardWidthY()
    {
        return BoardWidthY;
    }

    
}