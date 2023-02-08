using System.Collections.Generic;

namespace DimCity;

public class Menu
{
    public int? ActiveSection { get; set; }
    public int? ActiveTile { get; set; }

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
        if (ActiveTile > GetTileNames(ActiveSection.Value).Count) 
        {
            return;
        }
        ActiveTile++;
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
                    "grass",
                    "dirt",
                    "water",
                    "beach", 
                    "road", 
                    "grassWhole", 
                    "beachCornerES",
                    "beachCornerNE",
                    "beachCornerNW",
                    "beachCornerSW",
                    "beachE",
                    "beachES",
                    "beachNE",
                    "beachN",
                    "beachNW",
                    "beach",
                    "beachS",
                    "beachSW",
                    "beachW",
                    "grassWhole",
                    "hillE",
                    "hillES",
                    "hillNE",
                    "hillN",
                    "hillNW",
                    "hillS",
                    "hillSW",
                    "hillW",
                    "riverBankedES",
                    "riverBankedEW",
                    "riverBankedNE",
                    "riverBankedNS",
                    "riverBankedNW",
                    "riverBankedSW",
                    "riverES",
                    "riverEW",
                    "riverNE",
                    "riverNS",
                    "riverNW",
                    "riverSW",
                    "waterCornerES",
                    "waterCornerNE",
                    "waterCornerNW",
                    "waterCornerSW",
                    "waterE",
                    "waterES",
                    "waterNE",
                    "waterN",
                    "waterNW",
                    "water",
                    "waterS",
                    "waterSW",
                    "waterW",           
                    };
            case 1:
                return new List<string> {
                    "empty"
                };
            case 2:
                return new List<string> {
                    "road",
                    "roadES",
                    "roadEW",
                    "roadHill2E",
                    "roadHill2N",
                    "roadHill2S",
                    "roadHill2W",
                    "roadHillE",
                    "roadHillN",
                    "roadHillS",
                    "roadHillW",
                    "roadNE",
                    "roadNS",
                    "roadNW",
                    "road",
                    "roadSW",
                    "lotE",
                    "lotES",
                    "lotNE",
                    "lotN",
                    "lotNW",
                    "lotS",
                    "lotSW",
                    "lotW",
                    "endE",
                    "endN",
                    "endS",
                    "endW",
                    "exitE",
                    "exitN",
                    "exitS",
                    "exitW",
                    "crossroadESW",
                    "crossroadNES",
                    "crossroadNEW",
                    "crossroadNSW",
                    "crossroad",
                    "bridgeEW",
                    "bridgeNS",                
                };
            default:
                return new List<string>();
        }
    }
}