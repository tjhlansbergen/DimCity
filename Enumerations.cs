internal static class Enumerations
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
        Selection,

        // terraform
        Water,
        Mountains,
        Forest,

        // transportation
        Road,
        Rail,
        
        // utilities


        // zoning
        Residential,
        Commercial,
        Industrial,

        Power,
    }

    internal static BuildingType[] BuildingTypesByCategory(BuildingCategory category)
    {
        return category switch
        {
            BuildingCategory.Terraform => [BuildingType.Water, BuildingType.Mountains, BuildingType.Forest],
            BuildingCategory.Transportation => [BuildingType.Road, BuildingType.Rail],
            BuildingCategory.Utilities => [BuildingType.Power],
            BuildingCategory.Zoning => [BuildingType.Residential, BuildingType.Commercial, BuildingType.Industrial],
            _ => [BuildingType.Selection],
        };
    }

}