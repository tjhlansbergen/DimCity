using Raylib_cs;

internal class Transportation : DimGrid
{ 
    internal Transportation(Rect bounds, Console console, Font font, Action<BuildingTypes.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in BuildingTypes.BuildingTypesByCategory(BuildingTypes.BuildingCategory.Transportation))
        {
            switch (type)
            {
                case BuildingTypes.BuildingType.Road:
                    AddItem(type.ToString(),  () => onSelectionChanged(BuildingTypes.BuildingType.Road), Icons.Road);
                    break;
                case BuildingTypes.BuildingType.Rail:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Rail), Icons.Rail);
                    break;
            }
        }        
    }
}