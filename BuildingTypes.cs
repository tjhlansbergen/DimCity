using Raylib_cs;

internal static class BuildingTypes
{
    internal enum BuildingCategory
    {
        None,
        Terraform,
        Transportation,
        Utilities,
        Zoning,
    }

    internal enum BuildingType
    {
        // none
        Select,
        Bulldoze,

        // terraform
        Water,
        Mountains,
        Forest,

        // transportation
        Road,
        Rail,
        
        // utilities
        Power,

        // zoning
        Residential,
        Commercial,
        Industrial,


    }

    internal static BuildingType[] BuildingTypesByCategory(BuildingCategory category)
    {
        return category switch
        {
            BuildingCategory.Terraform => [BuildingType.Water, BuildingType.Mountains, BuildingType.Forest],
            BuildingCategory.Transportation => [BuildingType.Road, BuildingType.Rail],
            BuildingCategory.Utilities => [BuildingType.Power],
            BuildingCategory.Zoning => [BuildingType.Residential, BuildingType.Commercial, BuildingType.Industrial],
            _ => [BuildingType.Select, BuildingType.Bulldoze],
        };
    }

    internal static Color ColorForBuildingType(BuildingType buildingType)
    {
        return buildingType switch
        {
            BuildingType.Water => Colors.Water,
            BuildingType.Mountains => Colors.Mountains,
            BuildingType.Forest => Colors.Forest,
            BuildingType.Road => Colors.Road,
            BuildingType.Rail => Colors.Rail,
            BuildingType.Residential => Colors.Residential,
            BuildingType.Commercial => Colors.Commercial,
            BuildingType.Industrial => Colors.Industrial,
            BuildingType.Power => Colors.Power,
            _ => Colors.Background
        };
    }

}