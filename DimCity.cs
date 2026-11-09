using System.Numerics;

internal class DimCity
{
    // keep public for serialization
    public string? Name { get; set; }
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new();
    public int Day { get; set; } = 0;  


    private Timer? timer;

    // parameterless constructor for de-serialization
    public DimCity() {}

    public DimCity(string name)
    {
        Name = name;
    }

    public void StartTime()
    {
        timer = new Timer(TimerElapsed, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
    }

    private void TimerElapsed(object? state)
    {
        Day++;
    }
}