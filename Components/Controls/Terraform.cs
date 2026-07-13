using Raylib_cs;

internal class Terraform : DimGrid
{ 
    internal Terraform(Rect bounds, Console console, Font font, Action<Enumerations.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in Enumerations.BuildingTypesByCategory(Enumerations.BuildingCategory.Terraform))
        {
            switch (type)
            {
                case Enumerations.BuildingType.Water:
                    AddItem(type.ToString(),  () => onSelectionChanged(Enumerations.BuildingType.Water), Icons.Water);
                    break;
                case Enumerations.BuildingType.Mountains:
                    AddItem(type.ToString(), () => onSelectionChanged(Enumerations.BuildingType.Mountains), Icons.Mountains);
                    break;
                case Enumerations.BuildingType.Forest:
                    AddItem(type.ToString(), () => onSelectionChanged(Enumerations.BuildingType.Forest), Icons.Forest);
                    break;
            }
        }        
    }
}