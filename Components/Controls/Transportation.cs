using Raylib_cs;

internal class Transportation : DimGrid
{ 
    internal Transportation(Rect bounds, Console console, Font font) : base(bounds, console, font)
    {
        foreach (var type in Enumerations.BuildingTypesByCategory(Enumerations.BuildingCategory.Transportation))
        {
            switch (type)
            {
                case Enumerations.BuildingType.Road:
                    AddItem(type.ToString(), () => console.Write($"Selected {type}"), Icons.Road);
                    break;
                case Enumerations.BuildingType.Rail:
                    AddItem(type.ToString(), () => console.Write($"Selected {type}"), Icons.Rail);
                    break;
            }
        }        
    }
}