using System.Numerics;
using static Enumerations;

internal class DimCity
{
    internal Dictionary<Vector2, Tile> Tiles { get; private set; } = new();

    internal DimCity()
    {
        for (int i = 5; i < 10; i++)
        {
            // temporarily make some water
            Tiles.Add(new Vector2(i, 10), new Tile { Terra = BuildingType.Water });
            Tiles.Add(new Vector2(i+3, 11), new Tile { Terra = BuildingType.Water });
        }
    }
}