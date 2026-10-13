using static BuildingTypes;

public class Tile
{
    // keep public for serialization
    public BuildingType? BuildingType { get; set; } = null;
}