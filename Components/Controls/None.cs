using Raylib_cs;

internal class None : DimGrid
{
    internal None(Rect bounds, Console console, Font font, Action<BuildingTypes.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in BuildingTypes.BuildingTypesByCategory(BuildingTypes.BuildingCategory.None))
        {
            switch (type)
            {
                case BuildingTypes.BuildingType.Select:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Select), Icons.Select);
                    break;
                case BuildingTypes.BuildingType.Bulldoze:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Bulldoze), Icons.Bulldoze);
                    break;
            }
        }

        SelectedIndex = 0; // default to the first item (Selection)      
    }
}