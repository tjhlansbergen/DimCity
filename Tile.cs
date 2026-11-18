using Raylib_cs;
using static BuildingTypes;

public class Tile
{
    // keep public for serialization
    public BuildingType BuildingType { get; set; }

    public bool HasPower { get; set; }

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

            // 'preview' 
            if (activeBuildType == BuildingType.Power)
            {
                DimLib.DrawCircleWithOutline(bounds.Grow((int)(bounds.size.X * Constants.powerRadius)), Colors.Background, ColorForBuildingType(BuildingType));
            }
            return;
        }

        if (CategoryForBuildingType(BuildingType) == BuildingCategory.Transportation)   
        {
            DimLib.DrawRect(bounds.Shrink(1), ColorForBuildingType(BuildingType));    
            return;
        }

        if (CategoryForBuildingType(BuildingType) != BuildingCategory.None)
        {
            DimLib.DrawRect(bounds, ColorForBuildingType(BuildingType));
        }
        
    }
}