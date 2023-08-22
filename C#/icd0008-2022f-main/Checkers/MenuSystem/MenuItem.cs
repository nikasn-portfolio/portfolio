namespace MenuSystem;

public class MenuItem
{
    public string Title { get; set; }

    public string ShortCut { get; set; }

    public Func<string>? MethodToRun { get; set; }

    public MenuItem(string shortCut, string title, Func<string>? methodToRun)
    {
        this.Title = title;
        this.ShortCut = shortCut;
        this.MethodToRun = methodToRun;
    }

    public override string ToString()
    {
        return ShortCut + ") " + Title;
    }
}