using Raylib_cs;
using static BuildingTypes;

public class Tile
{
    // keep public for serialization
    public BuildingType BuildingType { get; set; }

    internal void Draw(Rect bounds, BuildingType? activeBuildType)
    {
        if (CategoryForBuildingType(BuildingType) == BuildingCategory.Zoning)
        {
            Raylib.DrawPixelV(bounds.Center(), ColorForBuildingType(BuildingType));
            return;
        }

        if (BuildingType == BuildingType.Power)
        {
            if (bounds.size.X > 5) bounds = bounds.Shrink(1);
            DimLib.DrawCircle(bounds, ColorForBuildingType(BuildingType));

            // advanced
            if (activeBuildType == BuildingType.Power)
            {
                DimLib.DrawCircleWithOutline(bounds.Grow(100), Colors.Background, ColorForBuildingType(BuildingType));
            }
            return;
        }

        if (BuildingType == BuildingType.Rail || BuildingType == BuildingType.Road)
        {
            DimLib.DrawRect(bounds.Shrink(1), ColorForBuildingType(BuildingType));    
            return;
        }

        DimLib.DrawRect(bounds, ColorForBuildingType(BuildingType));
    }
}