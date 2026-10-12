using Raylib_cs;

internal class Zoning : DimGrid
{
    internal Zoning(Rect bounds, Console console, Font font, Action<BuildingTypes.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in BuildingTypes.BuildingTypesByCategory(BuildingTypes.BuildingCategory.Zoning))
        {
            switch (type)
            {
                case BuildingTypes.BuildingType.Residential:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Residential), Icons.Residential);
                    break;
                case BuildingTypes.BuildingType.Commercial:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Commercial), Icons.Commercial);
                    break;
                case BuildingTypes.BuildingType.Industrial:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Industrial), Icons.Industrial);
                    break;
            }
        }
    }
}
