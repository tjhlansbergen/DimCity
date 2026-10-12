using Raylib_cs;

internal class Utilities : DimGrid
{
    internal Utilities(Rect bounds, Console console, Font font, Action<BuildingTypes.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in BuildingTypes.BuildingTypesByCategory(BuildingTypes.BuildingCategory.Utilities))
        {
            switch (type)
            {
                case BuildingTypes.BuildingType.Power:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Power), Icons.Power);
                    break;
            }
        }
    }
}
