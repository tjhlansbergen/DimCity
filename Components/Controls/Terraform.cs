using Raylib_cs;

internal class Terraform : DimGrid
{ 
    internal Terraform(Rect bounds, Console console, Font font, Action<BuildingTypes.BuildingType> onSelectionChanged) : base(bounds, console, font)
    {
        foreach (var type in BuildingTypes.BuildingTypesByCategory(BuildingTypes.BuildingCategory.Terraform))
        {
            switch (type)
            {
                case BuildingTypes.BuildingType.Water:
                    AddItem(type.ToString(),  () => onSelectionChanged(BuildingTypes.BuildingType.Water), Icons.Water);
                    break;
                case BuildingTypes.BuildingType.Mountains:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Mountains), Icons.Mountains);
                    break;
                case BuildingTypes.BuildingType.Forest:
                    AddItem(type.ToString(), () => onSelectionChanged(BuildingTypes.BuildingType.Forest), Icons.Forest);
                    break;
            }
        }        
    }
}