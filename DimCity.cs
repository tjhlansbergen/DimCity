using System.Numerics;

internal class DimCity
{
    // keep public for serialization
    public string? Name { get; set; }
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new();

    

    // parameterless constructor for de-serialization
    public DimCity() {}

    public DimCity(string name)
    {
        Name = name;
    }

}