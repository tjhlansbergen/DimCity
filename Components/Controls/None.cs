using Raylib_cs;

internal class None : DimGrid
{ 
    internal None(Rect bounds, Console console, Font font, Action<Enumerations.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in Enumerations.BuildingTypesByCategory(Enumerations.BuildingCategory.None))
        {
            switch (type)
            {
                case Enumerations.BuildingType.Selection:
                    AddItem(type.ToString(),  () => onSelectionChanged(Enumerations.BuildingType.Selection), Icons.Selection);
                    break;
            }
        }

        SelectedIndex = 0; // default to the first item (Selection)      
    }
}