using System.Numerics;
using Raylib_cs;

internal class DimSelection : DimView
{
    public BuildingTypes.BuildingType SelectedBuildingType { get; set; } = BuildingTypes.BuildingType.Select;
      
    internal DimSelection(Rect bounds, Console console) : base(bounds, console)
    {
        console.Write($"Selection mounted at {bounds}", debug: true);
    }

    internal override void Draw()
    {
        DimLib.DrawRect(Bounds, Colors.Panel);

        var iconBounds = Bounds.Shrink(5);
        IconForBuildingType(SelectedBuildingType)(iconBounds, true);
    }

    private Action<Rect, bool> IconForBuildingType(BuildingTypes.BuildingType buildingType)
    {
        return buildingType switch
        {
            BuildingTypes.BuildingType.Water => Icons.Water,
            BuildingTypes.BuildingType.Mountains => Icons.Mountains,
            BuildingTypes.BuildingType.Forest => Icons.Forest,
            BuildingTypes.BuildingType.Road => Icons.Road,
            BuildingTypes.BuildingType.Rail => Icons.Rail,
            BuildingTypes.BuildingType.Select => Icons.Select,
            BuildingTypes.BuildingType.Bulldoze => Icons.Bulldoze,
            _ => (_, _) => { /* do nothing */ }
        };
    }
}

