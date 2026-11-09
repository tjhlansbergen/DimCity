using static BuildingTypes;

public class Tile
{
    // keep public for serialization
    public BuildingType BuildingType { get; set; }

    internal void Draw(Rect bounds)
    {
        if (CategoryForBuildingType(BuildingType) == BuildingCategory.Zoning)
        {
            DimLib.DrawRectWithOutline(
                bounds,
                fillColor: Colors.Background,
                outlineColor: ColorForBuildingType(BuildingType),
                0.5f
            );
            return;
        }

        if (BuildingType == BuildingType.Power)
        {
            DimLib.DrawCircle(bounds, ColorForBuildingType(BuildingType));
            return;
        }

        if (BuildingType == BuildingType.Rail)
        {
            DimLib.DrawRect(bounds.Shrink(1), ColorForBuildingType(BuildingType));    
            return;
        }

        DimLib.DrawRect(bounds, ColorForBuildingType(BuildingType));
    }
}