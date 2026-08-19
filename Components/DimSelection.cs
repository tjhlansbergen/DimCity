using System.Numerics;
using Raylib_cs;

internal class DimSelection : DimView
{
    public Enumerations.BuildingType SelectedBuildingType { get; set; } = Enumerations.BuildingType.Select;
      
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

    private Action<Rect, bool> IconForBuildingType(Enumerations.BuildingType buildingType)
    {
        return buildingType switch
        {
            Enumerations.BuildingType.Water => Icons.Water,
            Enumerations.BuildingType.Mountains => Icons.Mountains,
            Enumerations.BuildingType.Forest => Icons.Forest,
            Enumerations.BuildingType.Road => Icons.Road,
            Enumerations.BuildingType.Rail => Icons.Rail,
            Enumerations.BuildingType.Select => Icons.Select,
            Enumerations.BuildingType.Bulldoze => Icons.Bulldoze,
            _ => (_, _) => { /* do nothing */ }
        };
    }
}

