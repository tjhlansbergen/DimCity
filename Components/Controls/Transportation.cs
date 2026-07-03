using Raylib_cs;

internal class Transportation : DimGrid
{ 
    internal Transportation(Rect bounds, Console console, Font font, Action<Enumerations.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in Enumerations.BuildingTypesByCategory(Enumerations.BuildingCategory.Transportation))
        {
            switch (type)
            {
                case Enumerations.BuildingType.Road:
                    AddItem(type.ToString(),  () => onSelectionChanged(Enumerations.BuildingType.Road), Icons.Road);
                    break;
                case Enumerations.BuildingType.Rail:
                    AddItem(type.ToString(), () => onSelectionChanged(Enumerations.BuildingType.Rail), Icons.Rail);
                    break;
            }
        }        
    }
}