internal static class Enumerations
{
    internal enum BuildingCategory
    {
        Transportation,
        Utilities,
        Zoning,
    }

    internal enum BuildingType
    {
        Selection,

        Road,
        Rail,
        
        Residential,
        Commercial,
        Industrial,

        Power,
    }

    internal static BuildingType[] BuildingTypesByCategory(BuildingCategory category)
    {
        return category switch
        {
            BuildingCategory.Transportation => [BuildingType.Road, BuildingType.Rail],
            BuildingCategory.Utilities => [BuildingType.Power],
            BuildingCategory.Zoning => [BuildingType.Residential, BuildingType.Commercial, BuildingType.Industrial],
            _ => [BuildingType.Selection],
        };
    }

}