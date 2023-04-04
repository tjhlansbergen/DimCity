namespace DimCity;

public static class Program
{
    public static void Main(string[] args)
    {
        using var game = new DimCity.DimGame(args.Length > 0 && args[0] == "-w");
        game.Run();
    }
}
