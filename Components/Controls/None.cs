using Raylib_cs;

internal class None : DimGrid
{
    internal None(Rect bounds, Console console, Font font, Action<Enumerations.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in Enumerations.BuildingTypesByCategory(Enumerations.BuildingCategory.None))
        {
            switch (type)
            {
                case Enumerations.BuildingType.Select:
                    AddItem(type.ToString(), () => onSelectionChanged(Enumerations.BuildingType.Select), Icons.Select);
                    break;
                case Enumerations.BuildingType.Bulldoze:
                    AddItem(type.ToString(), () => onSelectionChanged(Enumerations.BuildingType.Bulldoze), Icons.Bulldoze);
                    break;
            }
        }

        SelectedIndex = 0; // default to the first item (Selection)      
    }
}