using System.Collections.Generic;

namespace DimCity;

public class Menu
{
    public int? ActiveSection { get; set; } = 0;
    public int? ActiveTile { get; set; } = 0;


    public void Up()
    {
        if (ActiveSection == null)
        {
            return;
        }

        if (ActiveSection == 0)
        {
            ActiveSection = null;
            return;
        }

        ActiveSection--;
        ActiveTile = 0;
    }

    public void Down()
    {
        if (ActiveSection == null)
        {
            ActiveSection = 0;
            ActiveTile = 0;
            return;
        }

        if (ActiveSection == GetSections().Count - 1)
        {
            return;
        }

        ActiveSection++;
        ActiveTile = 0;
    }

    public void Left()
    {
        if (ActiveSection == null)
        {
            ActiveTile = null;
            return;
        }
        if (ActiveTile == null)
        {
            ActiveTile = 0;
            return;
        }
        if (ActiveTile == 0)
        {
            return;
        }
        ActiveTile--;
    }

    public void Right()
    {
        if (ActiveSection == null)
        {
            ActiveTile = null;
            return;
        }
        if (ActiveTile == null)
        {
            ActiveTile = 0;
            return;
        }
        if (ActiveTile > GetTileNames(ActiveSection.Value).Count - 2)
        {
            return;
        }
        ActiveTile++;
    }

    public string GetSelectedTileName()
    {
        if (ActiveSection == null || ActiveTile == null) return null;
        return GetTileNames(ActiveSection.Value)[ActiveTile.Value];
    }

    public static Dictionary<int, string> GetSections()
    {
        return new Dictionary<int, string>()
        {
            {0, "Terraforming"},
            {1, "Zoning"},
            {2, "Transportation"},
        };
    }

    public static List<string> GetTileNames(int section)
    {
        switch (section)
        {
            case 0:
                return new List<string> {
                    "road",
                    "dirt",
                    "water",
                    "beach",
                    "grass",
                    "grassWhole",
                    "beachCornerNE",
                    "beachNE",
                    "beachN",
                    "hillNE",
                    "hillN",
                    "riverBankedNE",
                    "riverBankedNS",
                    "riverNE",
                    "riverNS",
                    "waterCornerNE",
                    "waterNE",
                    "waterN",
                    };
            case 1:
                return new List<string> {
                    "empty"
                };
            case 2:
                return new List<string> {
                    "road",
                    "roadHill2N",
                    "roadHillN",
                    "roadNE",
                    "roadNS",
                    "lotNE",
                    "lotN",
                    "endN",
                    "exitN",
                    "crossroadESW",
                    "crossroad",
                    "bridgeNS",
                };
            default:
                return new List<string>();
        }
    }
}